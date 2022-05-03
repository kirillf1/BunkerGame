using BunkerGame.Application.Characters.GetCharacterCommand;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;
namespace BunkerGame.VkApi.VKCommands
{
    public class CharacterGetCommand : VkCommand
    {
        private readonly IConversationRepository conversationRepository;
        private readonly IMediator mediator;

        public CharacterGetCommand(IVkApi vkApi, IConversationRepository conversationRepository,
            IMediator mediator) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
            this.mediator = mediator;
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
                    await SendCharacter(userId, conversations.First().ConversationId);
                }
                else
                {
                    await SendVkMessage("Вы не состоите в игре!", userId, VkKeyboardFactory.BuildPersonalButtons());
                }
            }
            else if (text.Contains("персонаж для:"))
            {
                text = text.Replace("персонаж для:", "").TrimStart();
                var gameId = conversations.First(c => c.ConversationName.ToLower() == text).ConversationId;
                await SendCharacter(userId, gameId);
            }
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
