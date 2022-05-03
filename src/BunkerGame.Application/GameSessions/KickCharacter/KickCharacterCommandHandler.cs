using BunkerGame.Application.GameSessions.EmptyFreePlaceEvent;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using MediatR;

namespace BunkerGame.Application.GameSessions.KickCharacter
{
    public class KickCharacterCommandHandler : IRequestHandler<KickCharacterCommand,Character>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IMediator mediator;

        public KickCharacterCommandHandler(IGameSessionRepository gameSessionRepository,IMediator mediator)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.mediator = mediator;
        }
        public async Task<Character> Handle(KickCharacterCommand command, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSessionWithCharacters(command.GameSessionId);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            var character = gameSession.Characters.Find(c => c.Id == command.CharacterId);
            if (character == null)
                throw new ArgumentNullException($"Can't find character in {nameof(gameSession)}");
            character.ChangeLive(false);
            await gameSessionRepository.CommitChanges();
            if (gameSession.Characters.Count(c => c.IsAlive) <= gameSession.FreePlaceSize)
                await mediator.Publish(new EmptyFreePlaceNotificationMessage(gameSession.Id, gameSession.FreePlaceSize), cancellationToken);
            return character;
        }
    }
}
