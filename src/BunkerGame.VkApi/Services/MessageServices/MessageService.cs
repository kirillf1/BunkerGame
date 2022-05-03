using BunkerGame.VkApi.VKCommands;
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
            if (isConversation && Regex.IsMatch(message, @"\bбот\b|^!|^отмена|^правила", RegexOptions.IgnoreCase))
            {
                vkCommandType = vkCommandsConversation.FirstOrDefault(c => Regex.IsMatch(message, c.Key,RegexOptions.IgnoreCase)).Value;
            }
            else if (!isConversation)
            {
                vkCommandType = vkCommandsPersonal.FirstOrDefault(c => Regex.IsMatch(message, c.Key, RegexOptions.Singleline & RegexOptions.IgnoreCase)).Value
                    ?? typeof(AnswerCommand);
            }
            return vkCommandType;

        }
        private static Dictionary<string, Type> InitConversationCommands()
        {
            return new Dictionary<string, Type>
            {
                ["новую игру"] = typeof(CreateGameSessionCommand),
                ["исключить"] = typeof(KickCommand),
                ["итоги"] = typeof(EndGameSessionCommand),
                ["статистика"] = typeof(StatisticsCommand),
                ["количество мест"] = typeof(CharacterSizeCommand),
                //["бункер"] = typeof(BunkerChangeCommand),
                //["катаклизм"] = typeof(CatastropheChangeCommand),
                ["правила|отмена"] = typeof(AnswerCommand)
            };

        }
        private static Dictionary<string, Type> InitPersonalCommands()
        {
            return new Dictionary<string, Type>
            {
                ["характеристики|хар:"] = typeof(CharacteristicChangeCommand),
                ["персонаж|персонажа"] = typeof(CharacterGetCommand),
                [@"использовать карты|использовать карту №\d|карта на:"] = typeof(CardUseCommand),
                ["Выбрать игру|Беседа:"] = typeof(SetTargetConversationCommand),
                ["правила|отмена"] = typeof(AnswerCommand)
            };

        }
    }
}
