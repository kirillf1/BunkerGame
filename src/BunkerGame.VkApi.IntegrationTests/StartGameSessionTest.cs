using BunkerGame.Domain.Characters;
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
    public class StartGameSessionTest
    {
        [Fact]
        public async Task ExecuteStartGameCommand_ValidUserCount_GameStateStarted()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 5);
            var bagConversation = bag.Conversations[peerId];
            var userSender = bagConversation.Users.First().Id;
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;

            await messageService.SendCreateGameSessionCommand(userSender, peerId);
            await messageService.SendCreateCharactersCommand(bagConversation.Users.Select(c => c.Id));
            await messageService.SendStartGameCommand(userSender, peerId);
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            var gameSession = await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);
            var characters = await services.GetService<ICharacterRepository>()!.GetCharacters(c => c.GameSessionId == gameSession.Id); 

            Assert.NotNull(gameSession);
            Assert.True(gameSession.Characters.Count == 5);
            Assert.True(gameSession.GameState == GameState.Started);
            Assert.True(characters.Count() == 5);
        }
        [Fact]
        public async Task ExecuteStartGameCommand_WithoutCreatedCharacters_GameStatePreparation()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 5);
            var bagConversation = bag.Conversations[peerId];
            var userSender = bagConversation.Users.First().Id;
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;

            await messageService.SendCreateGameSessionCommand(userSender, peerId);
            await messageService.SendStartGameCommand(userSender, peerId);
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            var gameSession = await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);

            Assert.NotNull(gameSession);
            Assert.True(gameSession.GameState == GameState.Preparation);
            Assert.Contains(bag.GetParams().Where(c => c.PeerId == peerId).Select(t => t.Message), t =>t.Contains("Не получается начать игру",StringComparison.OrdinalIgnoreCase));
        }
    }
}
