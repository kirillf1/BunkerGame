using BunkerGame.Application.Players.AddNewPlayers;
using BunkerGame.Domain.Players;
using BunkerGame.VkApi.VKCommands;
using BunkerGame.VkApi.VKCommands.CancelKeyboardCommands;
using BunkerGame.VkApi.VKCommands.CardCommands;
using BunkerGame.VkApi.VKCommands.CharacterCountCommands;
using BunkerGame.VkApi.VKCommands.SetDifficultyCommands;
using BunkerGame.VkApi.VKCommands.SetTargetConversationCommands;
using MediatR;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        static MessageService()
        {
            vkCommandsConversation = InitConversationCommands();
            vkCommandsPersonal = InitPersonalCommands();
        }
        private static readonly Dictionary<string, Type> vkCommandsPersonal;
        private static readonly Dictionary<string, Type> vkCommandsConversation;

        private readonly IServiceScopeFactory serviceFactory;
        private readonly ILogger<MessageService> logger;

        public MessageService(IServiceScopeFactory serviceFactory, ILogger<MessageService> logger)
        {
            this.serviceFactory = serviceFactory;
            this.logger = logger;
        }
        public async Task SendMessage(Message message)
        {
            if (message.Action == null)
                await Answer(message);
            else if (message.Action.Type.ToString() == "chat_title_update")
                await UpdateConversationName(message.FromId!.Value);
            else if (message.Action.Type.ToString() == "chat_kick_user" || message.Action.Type.ToString() == "chat_invite_user")
                await UpdateConversationUsers(message.FromId!.Value);
        }
        private async Task Answer(Message message)
        {
            message.Text = Regex.Replace(message.Text, @"\[.+\]", "").TrimStart();
            bool isConversation = message.PeerId > 2000000000;
            var vkCommandType = FindVkCommandType(message.Text, isConversation);
            if (isConversation && vkCommandType == null)
            {
                return;
            }
            else if (!isConversation && vkCommandType == null)
            {
                logger.LogWarning("Can't find command for {message} message", message.Text);
                return;
            }
            try
            {
                using var serviceScope = serviceFactory.CreateScope();
                var command = (VkCommand)serviceScope.ServiceProvider.GetRequiredService(vkCommandType!);
                var result = await command.SendAsync(message);
                if (!result)
                    logger.LogWarning("Didn't send command: {commandName}, from message: {message}", nameof(command), message.Text);
            }
            catch (Exception ex)
            {
                logger.LogError("{Error} from messsage: {message}, source:{source}", ex.Message, message.Text, ex.Source);
            }
        }
        protected Type? FindVkCommandType(string message, bool isConversation)
        {
            Type? vkCommandType = null;
            // if conversation and message starts with "бот","отмена","правила" finds in conversation conversation commands
            if (isConversation && Regex.IsMatch(message, @"\bбот\b|^!|^отмена|^правила", RegexOptions.IgnoreCase))
            {
                vkCommandType = vkCommandsConversation.FirstOrDefault(c => Regex.IsMatch(message, c.Key, RegexOptions.IgnoreCase)).Value;
            }
            else if (!isConversation)
            {
                vkCommandType = vkCommandsPersonal.FirstOrDefault(c => Regex.IsMatch(message, c.Key, RegexOptions.Singleline & RegexOptions.IgnoreCase)).Value
                    ?? typeof(AnswerCommand);
            }
            return vkCommandType;

        }
        private async Task UpdateConversationUsers(long peerId)
        {
            using var serviceScope = serviceFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var conversationRepository = provider.GetRequiredService<IConversationRepository>();
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
                return;
            var vkApi = provider.GetRequiredService<IVkApi>();
            var users = await vkApi.Messages.GetConversationMembersAsync(peerId);
            conversation.Users.Clear();
            conversation.Users.AddRange(users.Profiles.Select(c => new ConversationRepositories.User(c.Id, c.FirstName, c.LastName)));
            await conversationRepository.UpdateConversation(conversation);
            var players = users.Profiles.Select(p => new Player(p.FirstName) { Id = p.Id, LastName = p.LastName });
            await provider.GetRequiredService<IMediator>().Send(new AddNewPlayersCommand(players));
        }
        private async Task UpdateConversationName(long peerId)
        {
            using var serviceScope = serviceFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var conversationRepository = provider.GetRequiredService<IConversationRepository>();
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
                return;
            var vkApi = provider.GetRequiredService<IVkApi>();
            var convResult = await vkApi.Messages.GetConversationsByIdAsync(new List<long> { peerId });
            conversation.ConversationName = convResult.Items.First().ChatSettings.Title;
            await conversationRepository.UpdateConversation(conversation);
        }
        private static Dictionary<string, Type> InitConversationCommands()
        {
            return new Dictionary<string, Type>
            {
                ["отмена"] = typeof(CancelConversationKeyboardCommand),
                ["новую игру"] = typeof(CreateGameSessionCommand),
                ["исключить"] = typeof(KickCommand),
                ["итоги"] = typeof(EndGameSessionCommand),
                ["статистика"] = typeof(StatisticsCommand),
                ["количество мест"] = typeof(GetCharacterSizeCommand),
                ["игроков:"] = typeof(ChangeCharactersCountCommand),
                ["количество игроков"] = typeof(GetAvailableCharactersCountCommand),
                ["сложность:"] = typeof(ChangeDifficultyCommand),
                ["установить сложность"] = typeof(GetAvailableDifficultiesCommand),
                ["правила"] = typeof(AnswerCommand)
            };

        }
        private static Dictionary<string, Type> InitPersonalCommands()
        {
            return new Dictionary<string, Type>
            {
                ["отмена"] = typeof(CancelPersonalKeyboardCommand),
                ["персонаж|персонажа"] = typeof(CharacterGetCommand),
                ["использовать карты"] = typeof(GetAvailableCardsCommand),
                [@"использовать карту №\d"] = typeof(TryUseCardCommand),
                ["карта на: "] = typeof(UseCardOnCharacterCommand),
                ["Выбрать игру"] = typeof(GetUserConversationsCommand),
                ["Беседа:"] = typeof(SetTargetConversationCommand),
                ["правила"] = typeof(AnswerCommand)
            };

        }
    }
}
