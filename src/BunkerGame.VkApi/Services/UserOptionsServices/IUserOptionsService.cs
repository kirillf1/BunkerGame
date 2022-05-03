namespace BunkerGame.VkApi.Services.UserOptionsServices
{
    public interface IUserOptionsService
    {
        /// <summary>
        /// Check games(conversations) if one or configured return true
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> CheckSinglenessGame(long userId);
        /// <summary>
        /// Get gameId where user play
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>if user in one room or value setted return conversation else return null</returns>
        public Task<Conversation?> GetUserGame(long userId);
        /// <summary>
        /// Sets gameId for user by UserOperationType SelectedGameId 
        /// </summary>
        /// <param name="gameSessionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task SetCurrentGame(long gameSessionId, long userId);
        /// <summary>
        /// Sets operation if UserOperationType exists rewrite it
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userOperationType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task SetOperation(long userId, UserOperationType userOperationType, string value);
        /// <summary>
        /// Get operation value and remove from repository except SelectedGameId
        /// </summary>
        public Task<string?> GetOperationValue(long userId, UserOperationType userOperationType);
    }
}
