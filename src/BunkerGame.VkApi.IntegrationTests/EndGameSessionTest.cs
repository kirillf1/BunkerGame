using BunkerGame.Domain.GameResults;
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
    public class EndGameSessionTest
    {
        [Fact]
        public async void EndGameSession_GameStarted_GameStateEnded()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 6);
            var conversationBag = bag.Conversations[peerId];
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;
            await messageService.CreateStartedGame(bag, peerId);
            await messageService.CreateStartedGame(bag, peerId);

            await messageService.SendMessage(new VkNet.Model.Message { PeerId = peerId, Text = "Бот, подвести итоги" });
            var gameSession = await GetGameSession(services, peerId);

            Assert.True(gameSession.GameState == GameState.Ended);
            Assert.Contains(bag.GetParams().Where(c => c.PeerId == peerId).Select(c => c.Message),
              t => t.Contains("Игра окончена", StringComparison.OrdinalIgnoreCase));
        }
        [Fact]
        public async void EndGameSessionAndGetStatistics_OneGameEnded_OneGamePlayed()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 6);
            var conversationBag = bag.Conversations[peerId];
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;
            await messageService.CreateStartedGame(bag, peerId);
            await messageService.CreateStartedGame(bag, peerId);

            await messageService.SendMessage(new VkNet.Model.Message { PeerId = peerId, Text = "Бот, подвести итоги" });
            await messageService.SendMessage(new VkNet.Model.Message { PeerId = peerId, Text = "Бот, статистика" });
            var gameSession = await GetGameSession(services, peerId);
            var gameResult = await services.GetService<IGameResultRepository>()!.GetGameResult(gameSession.Id);

            Assert.NotNull(gameResult);
            Assert.True(gameResult!.GetGamesCount() == 1);
            Assert.Contains("Игр всего", bag.GetParams().Last(c => c.PeerId == peerId).Message,
              StringComparison.OrdinalIgnoreCase);

        }
        private static async Task<GameSession> GetGameSession(ServiceProvider services, long peerId)
        {
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            return await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);
        }
    }
}
