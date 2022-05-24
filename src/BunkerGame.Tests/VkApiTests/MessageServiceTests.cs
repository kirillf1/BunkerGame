using BunkerGame.VkApi.Services.MessageServices;
using BunkerGame.VkApi.VKCommands;
using BunkerGame.VkApi.VKCommands.CancelKeyboardCommands;
using BunkerGame.VkApi.VKCommands.CardCommands;
using BunkerGame.VkApi.VKCommands.CharacterCountCommands;
using BunkerGame.VkApi.VKCommands.SetDifficultyCommands;
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
        [InlineData(typeof(GetAvailableDifficultiesCommand), "@club191848682 Бот,установить сложность", 2000000001)]
        public async void CheckVkCommandsViaText_Should_ExecuteCurrentCommand(Type cardCommand,string text,long peerId)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(cardCommand)).Returns(new Mock<VkCommand>());
            var logger = new Mock<ILogger<MessageService>>();
            var serviceScopeFactory = CreateMockServiceScopeFactory(serviceProvider.Object);
            IMessageService messageService = new MessageService(CreateMockServiceScopeFactory(serviceProvider.Object).Object, logger.Object);

            await messageService.SendMessage(new VkNet.Model.Message() { Text = text,PeerId =peerId});
            
            serviceProvider.Verify(c=>c.GetService(cardCommand), Times.Once);
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
