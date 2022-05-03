namespace BunkerGame.VkApi.UserOperationRepositories
{
    public interface IUserOperationRepository
    {
        public Task<string?> GetUserOperationValue(long userId, UserOperationType userOperationType);
        public Task AddOperationState(long userId, UserOperationType userOperationType, string value);
        public Task RemoveOperationState(long userId, UserOperationType userOperationType);
    }
}
