using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using MediatR;

namespace BunkerGame.Application.Characters.GetCharacterCommand
{
    public class GetCharacterCommandHandler : IRequestHandler<GetCharacterCommand, Character?>
    {
        private readonly IGameSessionRepository gameSessionRepository;

        public GetCharacterCommandHandler(IGameSessionRepository gameSessionRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
        }
       
        public async Task<Character?> Handle(GetCharacterCommand command, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSessionWithCharacters(command.GameSessionId);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            // if character was given
            var character = gameSession.Characters.FirstOrDefault(c => c.PlayerId == command.PlayerId);
            if (character != null)
                return character;
            character = gameSession.Characters.FirstOrDefault(c => c.PlayerId == null);
            if (character == null)
                return default;
            character.ChangeLive(true);
            character.SetPlayerId(command.PlayerId);
            await gameSessionRepository.CommitChanges();
            return character;
        }
    }
}
