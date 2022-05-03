namespace BunkerGame.VkApi.Services.UserOptionsServices
{
    public class UserOptionsService : IUserOptionsService
    {
        private readonly IUserOperationRepository userOperationRepository;
        private readonly IConversationRepository conversationRepository;

        public UserOptionsService(IUserOperationRepository userOperationRepository, IConversationRepository conversationRepository)
        {
            this.userOperationRepository = userOperationRepository;
            this.conversationRepository = conversationRepository;
        }
        public async Task<bool> CheckSinglenessGame(long userId)
        {
            var conversations = await conversationRepository.GetConversationsByUserId(userId);
            if (conversations.Count() == 1)
                return true;
            var value = await userOperationRepository.GetUserOperationValue(userId, UserOperationType.SelectedGameId);
            if (value == null)
                return false;
            if (!long.TryParse(value, out var gameId))
                return false;
            return conversations.Any(c => c.ConversationId == gameId);
        }

        public async Task<string?> GetOperationValue(long userId, UserOperationType userOperationType)
        {
            var value = await userOperationRepository.GetUserOperationValue(userId, userOperationType);
            if (value != null && userOperationType != UserOperationType.SelectedGameId)
                await userOperationRepository.RemoveOperationState(userId, userOperationType);
            return value;
        }

        public async Task<Conversation?> GetUserGame(long userId)
        {
            var value = await userOperationRepository.GetUserOperationValue(userId, UserOperationType.SelectedGameId);
            if (value == null)
            {
                var conversations = await conversationRepository.GetConversationsByUserId(userId);
                if (conversations.Count() == 1)
                    return conversations.First();
            }
            if (!long.TryParse(value, out var gameId))
                return default;
            return await conversationRepository.GetConversation(gameId);
        }

        public async Task SetCurrentGame(long gameSessionId, long userId)
        {
            await userOperationRepository.AddOperationState(userId, UserOperationType.SelectedGameId, gameSessionId.ToString());
        }

        public async Task SetOperation(long userId, UserOperationType userOperationType, string value)
        {
            await userOperationRepository.AddOperationState(userId, userOperationType, value);
        }
    }
}
