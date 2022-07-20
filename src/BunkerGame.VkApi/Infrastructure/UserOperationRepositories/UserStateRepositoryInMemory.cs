using Microsoft.Extensions.Caching.Memory;

namespace BunkerGame.VkApi.Infrastructure.UserOperationRepositories
{

    public class UserStateRepositoryInMemory : IUserOperationRepository
    {
        private const string UserOperationStorageName = "UserOperations";
        private readonly IMemoryCache memoryCache;
        private readonly TimeSpan operationLifeTime;

        public UserStateRepositoryInMemory(IMemoryCache memoryCache, TimeSpan operationLifeTime)
        {
            this.memoryCache = memoryCache;
            this.operationLifeTime = operationLifeTime;
        }
        public UserStateRepositoryInMemory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            operationLifeTime = TimeSpan.FromHours(1);
        }
        public Task AddOperationState(long userId, UserOperationType userOperationType, string value)
        {
            var userOpKey = $"{UserOperationStorageName}{userId}";
            if (!memoryCache.TryGetValue(userOpKey, out List<UserOperation> operations))
            {
                operations = new List<UserOperation>();
                operations.Add(new UserOperation(userOperationType, value));
                memoryCache.Set(userOpKey, operations, operationLifeTime);
            }
            var operation = operations.FirstOrDefault(c => c.UserOperationType == userOperationType);
            if (operation != null)
                operations.Remove(operation);
            operations.Add(new UserOperation(userOperationType, value));
            return Task.CompletedTask;
        }



        public Task<string?> GetUserOperationValue(long userId, UserOperationType userOperationType)
        {
            return Task.Run(() =>
            {
                var userOpKey = $"{UserOperationStorageName}{userId}";
                if (!memoryCache.TryGetValue(userOpKey, out List<UserOperation> operations))
                {
                    return default;
                }
                var operation = operations.FirstOrDefault(c => c.UserOperationType == userOperationType);
                if (operation == null)
                    return default;
                return operation.Value;
            });
        }

        public Task RemoveOperationState(long userId, UserOperationType userOperationType)
        {
            var userOpKey = $"{UserOperationStorageName}{userId}";
            if (!memoryCache.TryGetValue(userOpKey, out List<UserOperation> operations))
            {
                return Task.CompletedTask;
            }
            var operation = operations.FirstOrDefault(c => c.UserOperationType == userOperationType);
            if (operation != null)
                operations.Remove(operation);
            return Task.CompletedTask;
        }
    }
}
