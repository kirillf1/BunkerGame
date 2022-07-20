using BunkerGame.VkApi.VkGame.VKCommands;
using BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands;
using BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.CharacterCountCommands;
using BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.SetDifficultyCommands;
using BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands.CardCommands;
using BunkerGame.VkApi.VkGame.VkGameServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BunkerGame.Tests.VkApiTests
{
    public class MessageServiceTests
    {

        [Theory]
        [InlineData(typeof(GetAvailableCardsCommand), "использовать карты",1)]
        [InlineData(typeof(TryUseCardCommand), "использовать карту №1",1)]
        [InlineData(typeof(UseCardOnCharacterCommand), "карта на: Т Д",1)]
        [InlineData(typeof(UseCardOnCharacterCommand), "Бот,карта на: Т Д",1)]
        [InlineData(typeof(CancelConversationKeyboardCommand),"Бот,отмена", 20000000001)]
        [InlineData(typeof(EndGameSessionCommand),"Бот,итоги",20000000001)]
        [InlineData(typeof(GetAvailableCharactersCountCommand), "Бот,количество игроков", 2000000001)]
        [InlineData(typeof(GetAvailableDifficultiesCommand), "@club5515124 Бот,установить сложность", 2000000001)]
        [InlineData(typeof(GetCurrentBunker), "@club133333 Бот, показать бункер", 2000000001)]
        [InlineData(typeof(GetCurrentCatastrophe), "@club133333 Бот, показать катастрофу", 2000000001)]
        public async void SendMessage_CorrectRequest_CreateVkCommand(Type VKCommandType,string text,long peerId)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            // get empty command
            serviceProvider.Setup(x => x.GetService(VKCommandType)).Returns(new Mock<VkCommand>());
            var logger = new Mock<ILogger<MessageService>>();
            var serviceScopeFactory = CreateMockServiceScopeFactory(serviceProvider.Object);
            IMessageService messageService = new MessageService(CreateMockServiceScopeFactory(serviceProvider.Object).Object, logger.Object);

            await messageService.SendMessage(new VkNet.Model.Message() { Text = text,PeerId =peerId});
            
            serviceProvider.Verify(c=>c.GetService(VKCommandType), Times.Once);
        }
        [Theory]
        [InlineData(typeof(GetAvailableCardsCommand), "использовать карту", 20000000001)]
        [InlineData(typeof(TryUseCardCommand), "Бот,использовать карту №1", 20000000001)]
        [InlineData(typeof(UseCardOnCharacterCommand), "карта на: Т Д", 200000000001)]
        [InlineData(typeof(UseCardOnCharacterCommand), "Бот,карта на: Т Д", 200000000001)]
        [InlineData(typeof(EndGameSessionCommand), "Бот,итоги", 1)]
        [InlineData(typeof(GetAvailableCharactersCountCommand), "Бот,количество игроков", 1)]
        [InlineData(typeof(GetAvailableDifficultiesCommand), "@club191848682 Бот,установить сложность", 1)]
        [InlineData(typeof(GetAvailableDifficultiesCommand), "@club191848682 Бот,2442впвпва", 1)]
        public async void SendMessage_InvalidRequest_NotCreateCommand(Type ExpectedVKCommandType, string text, long peerId)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            IMessageService messageService = CreateMessageService(ExpectedVKCommandType,serviceProvider);

            await messageService.SendMessage(new VkNet.Model.Message() { Text = text, PeerId = peerId });

            serviceProvider.Verify(c => c.GetService(ExpectedVKCommandType), Times.Never);
        }
        private IMessageService CreateMessageService(Type VKCommandType,Mock<IServiceProvider> serviceProvider)
        {
           
            // get empty command
            serviceProvider.Setup(x => x.GetService(VKCommandType)).Returns(new Mock<VkCommand>());
            var logger = new Mock<ILogger<MessageService>>();
            var serviceScopeFactory = CreateMockServiceScopeFactory(serviceProvider.Object);
            IMessageService messageService = new MessageService(CreateMockServiceScopeFactory(serviceProvider.Object).Object, logger.Object);
            return messageService;
        }
        private Mock<IServiceScopeFactory> CreateMockServiceScopeFactory(IServiceProvider serviceProvider)
        {
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);
            return serviceScopeFactory;
        }
    }
}
