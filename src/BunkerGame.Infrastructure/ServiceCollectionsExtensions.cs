using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using BunkerGame.Domain.Players;
using BunkerGame.Infrastructure.Database;
using BunkerGame.Infrastructure.Domain.BunkerComponents;
using BunkerGame.Infrastructure.Domain.Catastrophes;
using BunkerGame.Infrastructure.Domain.CharacterComponents;
using BunkerGame.Infrastructure.Domain.Characters;
using BunkerGame.Infrastructure.Domain.ExternalSurrounings;
using BunkerGame.Infrastructure.Domain.GameResults;
using BunkerGame.Infrastructure.Domain.GameSessions;
using BunkerGame.Infrastructure.Domain.Players;
using BunkerGame.Infrastructure.JsonContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BunkerGame.Infrastructure
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration, bool inMemory)
        {
            if (inMemory)
            {
                configureInMemoryContext(serviceCollection);
            }
            else
            {
                var connectionString = configuration.GetConnectionString("PostgresConnectionString");
                serviceCollection.AddDbContext<BunkerGameDbContext>(options => options.UseNpgsql(connectionString));
            }
            AddRepositories(serviceCollection);
            return serviceCollection;
        }
        private static void configureInMemoryContext(IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddDbContext<BunkerGameDbContext>(opt => opt.UseInMemoryDatabase("BunkerGame"));
        }
        private static void AddRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(ICharacterComponentRepository<>), typeof(CharacterComponentRepositoryEFBase<>));
            serviceCollection.AddScoped<ICharacterComponentRepository<Size>, SizeMockRepository>();
            serviceCollection.AddScoped<ICharacterComponentRepository<Sex>, SexMockRepository>();
            serviceCollection.AddScoped<ICharacterComponentRepository<Age>, AgeMockRepository>();
            serviceCollection.AddScoped<ICharacterComponentRepository<Childbearing>, ChildbearingMockRepository>();

            serviceCollection.AddScoped<ICatastropheRepository, CatastropheRepositoryEf>();
            serviceCollection.AddScoped<ICharacterRepository, CharactersRepositoryEf>();
            serviceCollection.AddScoped(typeof(IBunkerComponentRepository<>), typeof(BunkerComponentRepositoryEf<>));
            serviceCollection.AddScoped<IGameResultRepository, GameResultRepositoryEf>();
            serviceCollection.AddScoped<IPlayerRepository, PlayerRepositoryEf>();
            serviceCollection.AddScoped<IGameSessionRepository, GameSessionRepositoryEf>();
            serviceCollection.AddScoped<IExternalSurroundingRepository, ExternalSurroundingsRepositoryEf>();

            serviceCollection.AddScoped<IBunkerComponentRepositoryLocator, BunkerComponentRepLocatorEf>();
            serviceCollection.AddScoped<ICharacterComponentRepLocator, CharacterComponentRepLocatorEf>();
        }
    }
}
