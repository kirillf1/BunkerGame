using Microsoft.Extensions.DependencyInjection;
using MediatR;
using BunkerGame.Application.Bunkers.BunkerFactories;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Characters;
using BunkerGame.Application.Characters.CharacterFactories;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Application.GameSessions.GameSessionsFactories;
using BunkerGame.Application.GameSessions.ResultCounters;
using BunkerGame.Application.Bunkers.ChangeBunkerComponent;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Application.Bunkers.ChangeBunkerComponent.ComponentHandlers;
using BunkerGame.Application.Characters.ExchangeCharacter;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Bunkers.ChangeBunkerComponent.ComponentCollectionHandlers;

namespace BunkerGame.Application
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            AddFactories(serviceCollection);
            AddBunkerChangeCommands(serviceCollection);
            AddExchangeCharacterComponentCommands(serviceCollection);
            AddChangeCharacterComponentCommands(serviceCollection);

            return serviceCollection;

        }
      
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
        private static void AddBunkerChangeCommands(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeBunkerComponentCommand<BunkerEnviroment>, BunkerEnviroment>), typeof(ChangeBunkerComponentCommandHandler<BunkerEnviroment>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeBunkerComponentCommand<BunkerWall>, BunkerWall>), typeof(ChangeBunkerComponentCommandHandler<BunkerWall>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeBunkerComponentCommand<BunkerSize>, BunkerSize>), typeof(ChangeBunkerSizeCommandHandler));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeBunkerComponentCommand<Supplies>, Supplies>), typeof(ChangeSuppliesCommandHandler));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeBunkerComponentCollectionCommand<BunkerObject>, IReadOnlyCollection<BunkerObject>>), typeof(ChangeBunkerComponentCollectionCommandHandler<BunkerObject>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeBunkerComponentCollectionCommand<ItemBunker>, IReadOnlyCollection<ItemBunker>>), typeof(ChangeBunkerComponentCollectionCommandHandler<ItemBunker>));
        }
        private static void AddExchangeCharacterComponentCommands(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Size>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Size>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Age>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Age>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Trait>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Trait>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<AdditionalInformation>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<AdditionalInformation>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Profession>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Profession>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Card>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Card>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Phobia>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Phobia>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<CharacterItem>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<CharacterItem>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Childbearing>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Childbearing>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Hobby>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Hobby>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Sex>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Sex>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ExchangeCharacteristicCommand<Health>, Tuple<Character, Character>>),
                typeof(ExchangeCharacteristicCommandHandler<Health>));

        }
        private static void AddChangeCharacterComponentCommands(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Size>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Size>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Age>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Age>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Sex>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Sex>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Profession>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Profession>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Hobby>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Hobby>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Phobia>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Phobia>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Health>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Health>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<CharacterItem>, Character>),
                typeof(ChangeCharacteristicCommandHandler<CharacterItem>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Card>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Card>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Trait>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Trait>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<AdditionalInformation>, Character>),
                typeof(ChangeCharacteristicCommandHandler<AdditionalInformation>));
            serviceCollection.AddScoped(typeof(IRequestHandler<ChangeCharacteristicCommand<Childbearing>, Character>),
                typeof(ChangeCharacteristicCommandHandler<Childbearing>));
        }
    }
}
