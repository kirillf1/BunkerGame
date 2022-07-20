using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.Cards;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.VkApi.VkGame.Characters;
using BunkerGame.VkApi.VkGame.GameResults;
using BunkerGame.VkApi.VkGame.GameSessions;
using BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers;
using BunkerGame.VkApi.VkGame.GameSessions.ResultCounters;
using BunkerGame.VkApi.VkGame.VKCommands;
using BunkerGame.VkApi.VkGame.VkGameServices;
using MediatR;
using System.Reflection;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame
{
    public static class ServiceApplicationCollectionsExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IVkApi>(servProv =>
            {
                var api = new VkNet.VkApi();
                api.Authorize(new ApiAuthParams { AccessToken = configuration["Config:AccessToken"] });
                return api;
            });
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
            serviceCollection.AddScoped<GameResultService>();
            serviceCollection.AddScoped<ResultCounterService>();
            serviceCollection.AddScoped<CharacterService>();
            serviceCollection.AddScoped<VkSenderByCharacter>();
            serviceCollection.AddScoped<ConversationService>();
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
            System.Reflection.Assembly.GetExecutingAssembly()
           .GetTypes()
           .Where(item => !item.IsAbstract && item.IsSubclassOf(typeof(VkCommand)))
           .ToList().ForEach(item => serviceCollection.AddScoped(item));
        }
    }
}
