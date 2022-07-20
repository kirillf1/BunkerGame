using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class CreateGameSessionHandler : GameSessionCommandHandlerBase<Commands.CreateGame>
    {
        private readonly IConversationRepository conversationRepository;

        public CreateGameSessionHandler(IConversationRepository conversationRepository, IGameSessionRepository gameSessionRepository,
            IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
            this.conversationRepository = conversationRepository;
        }

        public override async Task<Unit> Handle(Commands.CreateGame request, CancellationToken cancellationToken)
        {
            var gameSession = new GameSession(request.GameSessionId, request.PlayerId);
            await gameSessionRepository.AddGameSession(gameSession);
            var conversation = await conversationRepository.GetConversation(gameSession.Id);
            if (conversation != null)
            {
                gameSession.ChangeDifficulty(conversation.Difficulty);
                gameSession.ChangeName(conversation.ConversationName);
                gameSession.ChangeMaxCharactersInGame(conversation.PlayersCount);
            }
            await SaveEvents(gameSession);
            return Unit.Value;
        }
    }
}
