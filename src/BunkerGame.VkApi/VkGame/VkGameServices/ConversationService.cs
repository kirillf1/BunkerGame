using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.GameSessions;
using MediatR;
using VkNet.Model.Keyboard;

namespace BunkerGame.VkApi.VkGame.VkGameServices
{
    public class ConversationService
    {
        private readonly GameSessionService gameSessionService;
        private readonly IConversationRepository conversationRepository;

        public ConversationService(GameSessionService gameSessionService, IConversationRepository conversationRepository)
        {
            this.gameSessionService = gameSessionService;
            this.conversationRepository = conversationRepository;
        }
        public async Task AddLastUsedKeyboard(long id, MessageKeyboard messageKeboard)
        {
            var conversation = await GetConversation(id);
            await AddLastUsedKeyboard(conversation, messageKeboard);
        }
        public async Task AddLastUsedKeyboard(Conversation conversation, MessageKeyboard messageKeboard)
        {
            conversation.PushKeyboard(messageKeboard);
            await conversationRepository.UpdateConversation(conversation);
        }
        public async Task<bool> ConversationExists(long id)
        {
            var conversation = await conversationRepository.GetConversation(id);
            return conversation != null;
        }
        public async Task<Conversation> CreateConversation(long conversationId, User userCreator, string conversationName)
        {
            var gameSessionId = new GameSessionId(Guid.NewGuid());
            var newConversation = new Conversation(conversationId, gameSessionId, conversationName);
            newConversation.AddUser(userCreator);
            await conversationRepository.AddConversation(newConversation);
            await gameSessionService.Handle(new Domain.GameSessions.Commands.CreateGame(gameSessionId, userCreator.PlayerId));
            return newConversation;
        }
        public async Task RestartGame(long conversationId)
        {
            var conversation = await GetConversation(conversationId);
            var gameSessionId = conversation.GameSessionId;
            conversation.RestartGame();
            await conversationRepository.UpdateConversation(conversation);
            await gameSessionService.Handle(new Domain.GameSessions.Commands.RestartGame(gameSessionId));
        }
        public async Task HandleCommand(long conversationId, Func<GameSessionId, IRequest> createCommand)
        {
            var conversation = await GetConversation(conversationId);
            var gameSessionId = conversation.GameSessionId;
            await gameSessionService.Handle(createCommand(gameSessionId));
        }
        public async Task HandleCommand(IRequest request)
        {
            await gameSessionService.Handle(request);
        }
        public async Task ChangePlayersCount(long conversationId, byte count)
        {
            var conversation = await GetConversation(conversationId);
            conversation.SetPlayersCount(count);
            await gameSessionService.Handle(new Domain.GameSessions.Commands.ChangeMaxCharacterSize(conversation.GameSessionId, count));
        }
        public async Task ChangeDifficulty(long conversationId, Difficulty difficulty)
        {
            var conversation = await GetConversation(conversationId);
            if (conversation.Difficulty == difficulty)
                return;
            conversation.Difficulty = difficulty;
            await gameSessionService.Handle(new Domain.GameSessions.Commands.ChangeDifficulty(conversation.GameSessionId, difficulty));
        }
        public async Task<Conversation> GetConversation(long id)
        {
            var conversation = await conversationRepository.GetConversation(id);
            if (conversation == null)
                throw new ArgumentNullException(nameof(conversation));
            return conversation;
        } 
    }
}
