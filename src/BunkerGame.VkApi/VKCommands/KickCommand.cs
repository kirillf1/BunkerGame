using BunkerGame.Application.GameSessions.KickCharacter;
using BunkerGame.Domain.Characters;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class KickCommand : VkCommand
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly IMediator mediator;

        public KickCommand(IVkApi vkApi, ICharacterRepository characterRepository, IConversationRepository conversationRepository,
            IMediator mediator) : base(vkApi)
        {
            this.characterRepository = characterRepository;
            this.conversationRepository = conversationRepository;
            this.mediator = mediator;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            var text = message.Text.ToLower();
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
            {
                await SendVkMessage("Игры не существует", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                return false;
            }
            if (text.Contains("исключить персонажей", StringComparison.OrdinalIgnoreCase))
            {
                var aliveCharacters = await characterRepository.GetCharacters(16, false, c => c.IsAlive && c.GameSessionId == peerId);
                var aliveUsersNames = aliveCharacters.Join(conversation.Users, c => c.PlayerId, u => u.UserId, (_, c) => c.UserName);
                await SendVkMessage("Выберете игрока для исключения", peerId, VkKeyboardFactory.BuildOptionsButtoms(aliveUsersNames.ToList(), "!исключить: "));
                return true;

            }
            else if (text.Contains("исключить", StringComparison.OrdinalIgnoreCase))
            {
                text = text.Replace("!исключить: ", "");
                var user = conversation.Users.Find(c => c.UserName.Contains(text,StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    await SendVkMessage($"Игрока с именем {text} не существует", peerId);
                    return false;
                }
                var character = await characterRepository.GetCharacters(1, false, c => c.PlayerId == user.UserId);
                await mediator.Send(new KickCharacterCommand(peerId, character.First().Id));
                await SendVkMessage($"Игрок {user.UserName} исключен!", peerId);
            }

            return false;

        }
    }
}
