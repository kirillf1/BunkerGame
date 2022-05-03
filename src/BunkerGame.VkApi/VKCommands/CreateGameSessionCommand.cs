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
            if( message.Text.Contains("новую игру на",StringComparison.OrdinalIgnoreCase))
            {
                var matchNumber = Regex.Match(message.Text, @"\d+");
                if (!(matchNumber.Success && byte.TryParse(matchNumber.ValueSpan,out charactersCount)))
                {
                    await SendVkMessage("Введите количество игроков корректно", peerId);
                    return true;
                }
     
            }
            var usersTask = vkApi.Messages.GetConversationMembersAsync(peerId);
            var convNameTask = vkApi.Messages.GetConversationsByIdAsync(new List<long> { peerId });
            Task.WaitAll(usersTask, convNameTask);
            var users = usersTask.Result.Profiles;
            var conversationName = convNameTask.Result.Items.First().ChatSettings.Title;
            try
            {
                if(charactersCount == default)
                    charactersCount = (byte)users.Count;
                var gameSession = await mediator.Send(new CreateGameCommand(false,charactersCount, conversationName, peerId));
                await SendVkMessage(GameComponentsConventer.ConvertGameSession(gameSession), peerId,
                    VkKeyboardFactory.BuildConversationButtons(true));
                await conversationRepository.AddConversation(new VkApi.ConversationRepositories.Conversation(peerId, conversationName, users.
                    Select(c => new ConversationRepositories.User(c.Id, c.FirstName + " " + c.LastName)),charactersCount));
            }
            catch (InvalidOperationException)
            {
                await SendVkMessage("Игра уже существует, закончите старую игру!", peerId);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                await SendVkMessage("Слишком мало, много игроков в беседе (минимум 6, максимум 12). Напишите боту: новую игру на {количество} человек",peerId);
            }
            await AddNewPlayers(users);
            return true;
        }
        private async Task AddNewPlayers(IEnumerable<VkNet.Model.User> users)
        {
            var players = users.Select(u => new Player(u.FirstName) { Id = u.Id, LastName = u.LastName });
            await mediator.Send(new AddNewPlayersCommand(players));
        }
    }
}
