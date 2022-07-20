using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.Characters;
using MediatR;

namespace BunkerGame.VkApi.VkGame.VkGameServices
{
    public class UserService : IUserService
    {
        private readonly IUserOperationRepository userOperationRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly CharacterService characterService;

        public UserService(IUserOperationRepository userOperationRepository, IConversationRepository conversationRepository, CharacterService characterService)
        {
            this.userOperationRepository = userOperationRepository;
            this.conversationRepository = conversationRepository;
            this.characterService = characterService;
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

        public async Task<long> GetUserIdByCharacterId(CharacterId characterId)
        {
            var conversation = await conversationRepository.GetConversationByCharacterId(characterId);
            if (conversation == null)
                throw new ArgumentNullException(nameof(conversation));
            return conversation.Users.Find(c => c.CharacterId == characterId)!.UserId;
        }

        public async Task<IEnumerable<Conversation>> GetАvailableConversationsForUser(long userId)
        {
            return await conversationRepository.GetConversationsByUserId(userId);
        }

        public async Task HandleCharacterCommand(IRequest request)
        {
            await characterService.Handle(request);
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
