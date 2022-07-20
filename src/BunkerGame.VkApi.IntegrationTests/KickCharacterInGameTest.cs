using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.Infrastructure.ConversationRepositories;
using BunkerGame.VkApi.IntegrationTests.Infrastructure;
using BunkerGame.VkApi.VkGame.VkGameServices;
using Microsoft.Extensions.DependencyInjection;

namespace BunkerGame.VkApi.IntegrationTests
{
    public class KickCharacterInGameTest
    {
        [Fact]
        public async void KickCharacterCommand_TwoCharactersValidNames_CharactersStateKicked()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 6);
            var conversationBag = bag.Conversations[peerId];
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;

            await messageService.CreateStartedGame(bag, peerId);
            foreach (var userName in conversationBag.Users.Take(2).Select(c => c.FirstName + " " + c.LastName))
            {
                await messageService.SendKickCharacterCommand(peerId, userName);
            }
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            var gameSession = await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);

            Assert.True(gameSession.Characters.AsEnumerable().Count(c => c.IsKicked) == 2);
        }

        [Fact]
        public async void KickCharacterCommand_InvalidNamesInUsers_CharactersNotKicked()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 6);
            var conversationBag = bag.Conversations[peerId];
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;
            await messageService.CreateStartedGame(bag, peerId);

            await messageService.SendKickCharacterCommand(peerId, "UnknownName1");
            await messageService.SendKickCharacterCommand(peerId, "UnknownName2");
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            var gameSession = await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);

            Assert.True(gameSession.Characters.AsEnumerable().Count(c => c.IsKicked) == 0);
            Assert.Contains(bag.GetParams().Where(c => c.PeerId == peerId).Select(c => c.Message),
                t => t.Contains("Игрока с именем UnknownName1 не существует", StringComparison.OrdinalIgnoreCase));
        }
        [Fact]
        public async void KickCharacterCommand_GameNotStarted_CharactersNotKicked()
        {
            var peerId = new Random().NextInt64(2000000000, 10000000000);
            var bag = MessageBagFactory.CreateMessageBug(peerId, 6);
            var conversationBag = bag.Conversations[peerId];
            using var services = ServiceBuilder.GetServiceProvider(bag);
            var messageService = services.GetService<IMessageService>()!;

            await messageService.CreateStartedGame(bag, peerId);
            await messageService.SendCreateGameSessionCommand(conversationBag.Users.First().Id, peerId);
            await messageService.SendCreateCharactersCommand(conversationBag.Users.Select(c => c.Id));
            foreach (var userName in conversationBag.Users.Take(2).Select(c => c.FirstName + " " + c.LastName))
            {
                await messageService.SendKickCharacterCommand(peerId, userName);
            }
            var conversation = await services.GetService<IConversationRepository>()!.GetConversation(peerId);
            var gameSession = await services.GetService<IGameSessionRepository>()!.GetGameSession(conversation!.GameSessionId);
            
            Assert.True(gameSession.Characters.AsEnumerable().Count(c => c.IsKicked) == 0);
        }
    }
}
