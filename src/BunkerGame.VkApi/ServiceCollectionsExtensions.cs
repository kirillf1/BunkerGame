using BunkerGame.VkApi.Services.MessageServices;
using BunkerGame.VkApi.VKCommands;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddVkServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IVkApi>(servProv =>
            {
                var api = new VkNet.VkApi();
                api.Authorize(new ApiAuthParams { AccessToken = configuration["Config:AccessToken"] });
                return api;
            });
            AddRepositories(serviceCollection);
            AddVkCommands(serviceCollection);
            AddServices(serviceCollection);
            return serviceCollection;
        }
        private static void AddVkCommands(IServiceCollection serviceCollection)
        {
            System.Reflection.Assembly.GetExecutingAssembly()
           .GetTypes()
           .Where(item => !item.IsAbstract && item.IsSubclassOf(typeof(VkCommand)))
           .ToList().ForEach(item => serviceCollection.AddScoped(item));

        }
        private static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMessageService, MessageService>();
            serviceCollection.AddScoped<IUserOptionsService, UserOptionsService>();
          
        }
        private static void AddRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IConversationRepository, ConversationRepositoryInMemory>();
            serviceCollection.AddScoped<IUserOperationRepository, UserStateRepositoryInMemory>();
        }
      
    }
}
