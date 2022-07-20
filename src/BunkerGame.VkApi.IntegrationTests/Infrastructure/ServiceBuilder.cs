using BunkerGame.Domain;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.Cards;
using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.Domain.Players;
using BunkerGame.Framework;
using BunkerGame.VkApi.Infrastructure.CharacterRepositories;
using BunkerGame.VkApi.Infrastructure.ConversationRepositories;
using BunkerGame.VkApi.Infrastructure.EventStores;
using BunkerGame.VkApi.Infrastructure.GameResultRepositories;
using BunkerGame.VkApi.Infrastructure.GameSessionRepositories;
using BunkerGame.VkApi.Infrastructure.PlayersRepository;
using BunkerGame.VkApi.Infrastructure.UnitOfWorks;
using BunkerGame.VkApi.Infrastructure.UserOperationRepositories;
using BunkerGame.VkApi.VkGame.Characters;
using BunkerGame.VkApi.VkGame.GameResults;
using BunkerGame.VkApi.VkGame.GameSessions;
using BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers;
using BunkerGame.VkApi.VkGame.GameSessions.ResultCounters;
using BunkerGame.VkApi.VkGame.VKCommands;
using BunkerGame.VkApi.VkGame.VkGameServices;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Infrastructure.Database.GameComponentContext;
using BunkerGameComponents.Infrastructure.Domain;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.IntegrationTests.Infrastructure
{
    public static class ServiceBuilder
    {

        public static ServiceProvider GetServiceProvider(MessageBag messageBag)
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddInfrastructure();
            services.AddApplication();
            services.AddLogging();
            services.AddSingleton<IVkApi>(_ => new VkApiMessageContainer(messageBag));
            return services.BuildServiceProvider();
        }
        private static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWorkInMemory>();
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
        private static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {

            AddServices(serviceCollection);
            AddFactories(serviceCollection);
            serviceCollection.AddMediatR(typeof(GameSessionCommandHandlerBase<>).GetTypeInfo().Assembly);
            AddVkCommands(serviceCollection);
            return serviceCollection;
        }
        private static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMessageService, MessageService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<GameSessionService>();
            serviceCollection.AddScoped<ResultCounterService>();
            serviceCollection.AddScoped<CharacterService>();
            serviceCollection.AddScoped<VkSenderByCharacter>();
            serviceCollection.AddScoped<ConversationService>();
            serviceCollection.AddScoped<GameResultService>();
        }
        private static void AddFactories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICharacterFactory, CharacterFactory>();
            serviceCollection.AddScoped<ICardFactory, CardFactory>();
            serviceCollection.AddScoped<IBunkerFactory, BunkerFactory>();
            serviceCollection.AddScoped<CharacterComponentGenerator>();
        }
        private static void AddVkCommands(IServiceCollection serviceCollection)
        {
            typeof(VkCommand).Assembly
           .GetTypes()
           .Where(item => !item.IsAbstract && item.IsSubclassOf(typeof(VkCommand)))
           .ToList().ForEach(item => serviceCollection.AddScoped(item));
        }
    }
}
