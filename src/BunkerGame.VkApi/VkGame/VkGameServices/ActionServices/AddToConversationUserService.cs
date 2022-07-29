using BunkerGame.Domain;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.VkGameServices.ActionServices
{
    public class AddToConversationUserService
    {
        private readonly IVkApi vkApi;
        private readonly IConversationRepository conversationRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly ILogger<AddToConversationUserService> logger;
        private readonly IUnitOfWork unitOfWork;

        public AddToConversationUserService(IVkApi vkApi, IConversationRepository conversationRepository, IPlayerRepository playerRepository,
            ILogger<AddToConversationUserService> logger,IUnitOfWork unitOfWork)
        {
            this.vkApi = vkApi;
            this.conversationRepository = conversationRepository;
            this.playerRepository = playerRepository;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }
        public async Task AddInConversation(long peerId, long userId)
        {
            var conversationTask = conversationRepository.GetConversation(peerId);
            var userTask = vkApi.Users.GetAsync(new long[] { userId });
            await Task.WhenAll(userTask, conversationTask);
            var conversation = conversationTask.Result;
            if (conversation == null)
                return;
            var user = userTask.Result.First();
            if (!conversation.Users.Any(c => c.UserId == userId))
            {
                var playerId = new PlayerId(Guid.NewGuid());
                await playerRepository.AddPlayer(new Player(playerId, user.FirstName) { LastName = user.LastName });
                conversation.AddUser(new User(user.Id, playerId));
                await conversationRepository.UpdateConversation(conversation);
                logger.LogInformation("User with id {id} added in conversation with id {conversationId}", user.Id, peerId);
                await unitOfWork.Save(default);
            }
        }
    }
}
