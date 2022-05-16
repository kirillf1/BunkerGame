using BunkerGame.Application.GameSessions.CreateGameSession;
using BunkerGame.Application.Players.AddNewPlayers;
using BunkerGame.Domain.Players;
using MediatR;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class CreateGameSessionCommand : VkCommand
    {

        private readonly IConversationRepository conversationRepository;
        private readonly IMediator mediator;

        public CreateGameSessionCommand(IVkApi vkApi, IConversationRepository conversationRepository, IMediator mediator) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
            this.mediator = mediator;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            if (peerId == 0)
            {
                await SendVkMessage("Данная команда выполняется только в беседе!", message.FromId.GetValueOrDefault(), VkKeyboardFactory.BuildPersonalButtons());
                return true;
            }
            byte charactersCount = default;
            if (message.Text.Contains("новую игру на", StringComparison.OrdinalIgnoreCase))
            {
                var matchNumber = Regex.Match(message.Text, @"\d+");
                if (!(matchNumber.Success && byte.TryParse(matchNumber.ValueSpan, out charactersCount)))
                {
                    await SendVkMessage("Введите количество игроков корректно", peerId);
                    return true;
                }

            }
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
            {
                conversation = await ConversationRepositories.Conversation.CreateConversation(vkApi, peerId);
                await conversationRepository.AddConversation(conversation);
                await AddNewPlayers(conversation.Users);
            }
            try
            {
                if (charactersCount == default)
                    charactersCount = conversation.PlayersCount;
                var gameSession = await mediator.Send(new CreateGameCommand(false, charactersCount, conversation.ConversationName, peerId));
                await SendVkMessage(GameComponentsConventer.ConvertGameSession(gameSession), peerId,
                    VkKeyboardFactory.BuildConversationButtons(true));
            }
            catch (InvalidOperationException)
            {
                await SendVkMessage("Игра уже существует, закончите старую игру!", peerId);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                await SendVkMessage("Слишком мало, много игроков в беседе (минимум 6, максимум 12). Напишите боту: новую игру на {количество} человек", peerId);
            }
            return true;
        }
        private async Task AddNewPlayers(IEnumerable<ConversationRepositories.User> users)
        {
            var players = users.Select(u => new Player(u.FirstName) { Id = u.UserId, LastName = u.LastName });
            await mediator.Send(new AddNewPlayersCommand(players));
        }
    }
}
