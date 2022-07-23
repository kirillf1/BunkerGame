using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.Infrastructure.ConversationRepositories;
using BunkerGame.VkApi.IntegrationTests.Infrastructure;
using BunkerGame.VkApi.VkGame.VkGameServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.VkApi.IntegrationTests
{
    public class CreateGameSessionTest
    {
        private const string commandText = "Бот,создать новую игру";
        [Fact]
        public async void CreateGameCommand_GameNotCreated_NewGameSession()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 6);
            var userSender = bag.Conversations[peerId].Users.First();
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;

            await messageService.SendMessage(new VkNet.Model.Message() { PeerId = peerId, FromId = userSender.Id, Text = commandText });
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            var gameSession = await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);

            Assert.NotNull(gameSession);
            Assert.True(conversation.Users.Count == 6);
            Assert.True(gameSession.Name == conversation.ConversationName);
            Assert.True(gameSession.GameState == GameState.Preparation);
        }
        [Fact]
        public async void CreateGameCommand_GameCreated_GameSessionRestarted()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 5);
            var bagConversation = bag.Conversations[peerId];
            var userSender = bagConversation.Users.First().Id;
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;

            await messageService.SendStartGameCommand(userSender, peerId);
            await messageService.SendCreateGameSessionCommand(userSender, peerId);
            
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            var gameSession = await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);
            Assert.True(gameSession.GameState == GameState.Preparation);
            Assert.Empty(gameSession.ExternalSurroundings);
            Assert.True(gameSession.Catastrophe == Catastrophe.DefaultCatastrophe);
            Assert.Empty(gameSession.Characters);

        }
    }
}
