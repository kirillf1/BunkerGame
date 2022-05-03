using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using BunkerGame.Application.Bunkers.BunkerFactories;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Characters;
using BunkerGame.Application.Characters.CharacterFactories;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Application.GameSessions.GameSessionsFactories;
using BunkerGame.Application.GameSessions.ResultCounters;

using MediatR.Registration;

namespace BunkerGame.Application
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            AddFactories(serviceCollection);
            
            //var assembly = typeof(ApplicationAssembly).GetTypeInfo().Assembly;
            //serviceCollection.AddMediatR(assembly);
            return serviceCollection;

        }
        //private static void ConfigureMediator(IServiceCollection serviceCollection)
        //{
        //    var serviceConfig = new MediatRServiceConfiguration();
        //    ServiceRegistrar.AddRequiredServices(serviceCollection, serviceConfig);
        //}
        private static void AddFactories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBunkerFactory, BunkerFactoryBase>();
            serviceCollection.AddScoped<ICharacterFactory, CharacterFactoryBase>();
            serviceCollection.AddScoped<IGameSessionFactory, GameSessionFactoryBase>();
            serviceCollection.AddScoped<IResultCounterFactory, ResultCounterFactory>();
        }
        //private static void AddRepositories(IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddScoped<IBunkerComponentRepositoryLocator, BunkerComponentRepositoryLocator>();
        //    serviceCollection.AddScoped<ICharacterComponentRepLocator, CharacterComponentRepServiceLocator>();
        //}
    }
}
