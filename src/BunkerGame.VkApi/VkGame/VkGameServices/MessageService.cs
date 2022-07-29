using BunkerGame.VkApi.VkGame.VKCommands;
using BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands;
using BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.CharacterCountCommands;
using BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.SetDifficultyCommands;
using BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.SetTargetConversationCommands;
using BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands;
using BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands.CardCommands;
using BunkerGame.VkApi.VkGame.VkGameServices.ActionServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VkGameServices
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
        private readonly Stopwatch stopwatch;
        private readonly IServiceScopeFactory serviceFactory;
        private readonly ILogger<MessageService> logger;

        public MessageService(IServiceScopeFactory serviceFactory, ILogger<MessageService> logger)
        {
            this.serviceFactory = serviceFactory;
            this.logger = logger;
            stopwatch = new();
        }
        public async Task SendMessage(Message message)
        {
            if (message.Action == null)
                await Answer(message);
            else if (message.Action.Type.ToString() == "chat_kick_user")
                await KickUser(message.PeerId!.Value, message.Action.MemberId!.Value);
            else if (message.Action.Type.ToString() == "chat_invite_user" && message.Action.MemberId.GetValueOrDefault() < 0)
                await SendGreteengsToConversation(message.PeerId!.Value);
            else if (message.Action.Type.ToString() == "chat_invite_user")
                await AddUser(message.PeerId!.Value, message.Action.MemberId!.Value);
            else if (message.Action.Type.ToString() == "chat_title_update")
                await UpdateConversationName(message.PeerId!.Value, message.Action.Text);
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
                StartRecordTime();
                var result = await command.SendAsync(message);
                if (!result)
                {
                    logger.LogWarning("Didn't send command: {commandName}, from message: {message}", nameof(command), message.Text);
                    return;
                }
                LogCommandExecutionTime(command.GetType().Name);
            }
            catch (Exception ex)
            {
                logger.LogError("{Error} from messsage: {message}, source:{source}", ex.Message, message.Text, ex.Source);
            }
        }
        protected Type? FindVkCommandType(string message, bool isConversation)
        {
            // if conversation and message starts with "бот","отмена","правила" finds in conversation commands
            if (isConversation && Regex.IsMatch(message, @"\bбот\b|^!|^отмена|^правила", RegexOptions.IgnoreCase))
            {
                return vkCommandsConversation.FirstOrDefault(c => Regex.IsMatch(message, c.Key, RegexOptions.IgnoreCase)).Value;
            }
            else if (!isConversation)
            {
                return vkCommandsPersonal.FirstOrDefault(c => Regex.IsMatch(message, c.Key, RegexOptions.Singleline & RegexOptions.IgnoreCase)).Value
                    ?? typeof(AnswerCommand);
            }
            return null;

        }
        private async Task SendGreteengsToConversation(long peerId)
        {
            using var serviceScope = serviceFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var service = provider.GetRequiredService<InvitedInConversationService>();
            await service.SendGreeting(peerId);
        }
        private async Task AddUser(long peerId,long userId)
        {
            using var serviceScope = serviceFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var service = provider.GetRequiredService<AddToConversationUserService>();
            await service.AddInConversation(peerId, userId);
        }
        private async Task KickUser(long peerId,long userId)
        {
            using var serviceScope = serviceFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var service = provider.GetRequiredService<KickFromConversationService>();
            await service.KickFromConversation(peerId, userId);
        }
        private async Task UpdateConversationName(long peerId, string newName)
        {
            using var serviceScope = serviceFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var service = provider.GetRequiredService<UpdateConversationNameService>();
            await service.UpdateName(newName,peerId);
        }
        private void StartRecordTime()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
        private void LogCommandExecutionTime(string commandName)
        {
            stopwatch.Stop();
            var seconds = stopwatch.Elapsed.TotalSeconds;
            logger.Log(LogLevel.Information, "Command {commandName} executed in {time} seconds", commandName, seconds);
        } 
        private static Dictionary<string, Type> InitConversationCommands()
        {
            return new Dictionary<string, Type>
            {
                ["отмена"] = typeof(CancelConversationKeyboardCommand),
                ["новую игру"] = typeof(CreateGameSessionCommand),
                ["исключить"] = typeof(KickCommand),
                ["итоги"] = typeof(EndGameSessionCommand),
                ["стартовать игру"] = typeof(StartGameCommand),
                ["показать катастрофу"] = typeof(GetCurrentCatastrophe),
                ["показать бункер"] = typeof(GetCurrentBunker),
                ["статистика"] = typeof(StatisticsCommand),
                ["количество мест"] = typeof(GetAvailableSizeCommand),
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
                ["персонаж|персонажа"] = typeof(GetCharacterCommand),
                ["Раскрыть характеристики|!Хар:"] = typeof(UncoverCharacterComponentCommand),
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
