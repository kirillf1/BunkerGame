using BunkerGame.Application.Characters.GetCharacterCommand;
using BunkerGame.Domain.Players;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;
using Conversation = BunkerGame.VkApi.ConversationRepositories.Conversation;

namespace BunkerGame.VkApi.VKCommands
{
    public class CharacterGetCommand : VkCommand
    {
        private readonly IConversationRepository conversationRepository;
        private readonly IMediator mediator;
        private readonly IPlayerRepository playerRepository;

        public CharacterGetCommand(IVkApi vkApi, IConversationRepository conversationRepository,
            IMediator mediator, IPlayerRepository playerRepository) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
            this.mediator = mediator;
            this.playerRepository = playerRepository;
        }
        public override async Task<bool> SendAsync(Message message)
        {
            var text = message.Text.ToLower();
            var userId = message.FromId.GetValueOrDefault();
            if (userId == 0)
                return false;
            var conversations = await conversationRepository.GetConversationsByUserId(userId);
            if (text.Contains("получить персонажа", StringComparison.OrdinalIgnoreCase))
            {
                if (conversations.Count() > 1)
                {
                    await SendVkMessage("Выберете беседу в которой будет Вам присвоен персонаж", userId,
                        VkKeyboardFactory.BuildOptionsButtoms(conversations.Select(c => c.ConversationName).ToList(), "Персонаж для: "));
                    return true;
                }
                else if (conversations.Count() == 1)
                {
                    var conversation = conversations.First();
                    await TryAddPlayerInRepository(userId, conversation);
                    await SendCharacter(userId, conversation.ConversationId);
                    return true;
                }
                else
                {
                    await SendVkMessage("Вы не состоите в игре!", userId, VkKeyboardFactory.BuildPersonalButtons());
                    return true;
                }
            }
            else if (text.Contains("персонаж для:"))
            {
                text = text.Replace("персонаж для:", "").TrimStart();
                var conversation = conversations.First(c => c.ConversationName.ToLower() == text);
                await TryAddPlayerInRepository(userId, conversation);
                await SendCharacter(userId, conversation.ConversationId);
                return true;
            }
            return false;
        }
        private async Task<bool> TryAddPlayerInRepository(long userId, Conversation conversation)
        {
            if (await playerRepository.PlayerAny(c => c.Id == userId))
                return false;
            var user = conversation.Users.First(c => c.UserId == userId);
            await playerRepository.AddPlayer(new Player(user.FirstName) { Id = userId, LastName = user.LastName });
            await playerRepository.CommitChanges();
            return true;
        }
        private async Task SendCharacter(long userId, long conversationId)
        {
            var character = await mediator.Send(new GetCharacterCommand(conversationId, userId));
            if (character == null)
                await SendVkMessage("Все персонажи заняты", userId);
            else
                await SendVkMessage(GameComponentsConventer.ConvertCharacter(character), userId, VkKeyboardFactory.BuildPersonalButtons());
        }
    }
}
