namespace BunkerGame.VkApi.Infrastructure.UserOperationRepositories
{
    internal class UserOperation
    {
        public UserOperation(UserOperationType userOperationType, string value)
        {
            UserOperationType = userOperationType;
            Value = value;
        }

        public UserOperationType UserOperationType { get; }
        public string Value { get; }
    }
    public enum UserOperationType
    {
        None,
        SelectedGameId,
        CharacteristicChange,
        ConversationOperation,
        CardNumber

    }
}
