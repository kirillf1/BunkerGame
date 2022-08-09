using BunkerGame.Domain;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.Framework;
using BunkerGame.VkApi.Infrastructure.CharacterRepositories;
using BunkerGame.VkApi.Infrastructure.EventStores;
using BunkerGame.VkApi.Infrastructure.GameResultRepositories;
using BunkerGame.VkApi.Infrastructure.GameSessionRepositories;
using BunkerGame.VkApi.Infrastructure.PlayersRepository;
using BunkerGame.VkApi.Infrastructure.UnitOfWorks;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Infrastructure.Database.GameComponentContext;
using BunkerGameComponents.Infrastructure.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace BunkerGame.VkApi.Infrastructure
{
    public static class ServiceInfrastructureCollectionsExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddScoped<Domain.IUnitOfWork, UnitOfWorkInMemory>();
            serviceCollection.AddScoped<IEventStore, EnventStoreInMemory>();
            serviceCollection.AddScoped(c => new GameComponentJsonContext(Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure", "GameComponentsJson")));
            AddRepositories(serviceCollection);
            return serviceCollection;
        }
        private static void AddRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IConversationRepository, ConversationRepositoryInMemory>();
            serviceCollection.AddScoped<IUserOperationRepository, UserStateRepositoryInMemory>();
            serviceCollection.AddScoped<IGameSessionRepository>(c =>
                    new GameSessionRepositoryChache(c.GetRequiredService<IMemoryCache>(), TimeSpan.FromHours(1)));
            serviceCollection.AddScoped<ICharacterRepository, CharacterRepositoryChache>();
            serviceCollection.AddScoped<IPlayerRepository, PlayersRepositoryChache>();
            serviceCollection.AddScoped(typeof(IGameComponentRepository<>), typeof(GameComponentRepositoryJson<>));
            serviceCollection.AddScoped<IGameComponentsRepository, GameComponentsRepositoryJson>();
            serviceCollection.AddScoped<IGameResultRepository, GameResultRepositoryInMemory>();
        }
    }
}
