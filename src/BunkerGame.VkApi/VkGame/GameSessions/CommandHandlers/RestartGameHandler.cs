using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;
using Commands = BunkerGame.Domain.GameSessions.Commands;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class RestartGameWithDeleteCharactersHandler : GameSessionCommandHandlerBase<Commands.RestartGame>
    {
        private readonly ICharacterRepository characterRepository;

        public RestartGameWithDeleteCharactersHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore,ICharacterRepository characterRepository) : base(gameSessionRepository, eventStore)
        {
            this.characterRepository = characterRepository;
        }

        public override async Task<Unit> Handle(Commands.RestartGame request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            gameSession.Restart();
            await SaveEvents(gameSession);
            await TryRemoveCharacters(gameSession);
            return Unit.Value;
        }
        private async Task TryRemoveCharacters(GameSession gameSession)
        {
            foreach (var characterGame in gameSession.Characters)
            {
                try
                {
                    var character = await characterRepository.GetCharacter(characterGame.Id);
                    await characterRepository.RemoveCharacter(character);
                }
                catch
                {
                }
            }
        }
    }
}
