using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public class ResultCounterService
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly ICharacterRepository characterRepository;
        private readonly IPlayerRepository playerRepository;

        public ResultCounterService(IGameSessionRepository gameSessionRepository, ICharacterRepository characterRepository, IPlayerRepository playerRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.characterRepository = characterRepository;
            this.playerRepository = playerRepository;
        }
        public async Task<ResultReport> CalculateResult(GameSessionId gameSessionId)
        {
            var gameSession = await gameSessionRepository.GetGameSession(gameSessionId);
            return await CalculateResult(gameSession);
        }
        public async Task<ResultReport> CalculateResult(GameSession gameSession)
        {
            var characters = await GetCharactersNotKickedWithNames(gameSession.Characters);
            var counterParams = new ResultCounterParams(characters, gameSession.Bunker, gameSession.ExternalSurroundings, gameSession.Catastrophe);
            var gameResultFactory = new GameResultCounterFactory();
            return gameResultFactory.GetGameResultCounter(counterParams, gameSession.Difficulty)
                .CalculateGameResut();
        }
        private async Task<IEnumerable<CharacterWithName>> GetCharactersNotKickedWithNames(IEnumerable<CharacterGame> charactersInGame)
        {
            var notKickedCharacters = charactersInGame.Where(c => !c.IsKicked).Select(c => new { c.PlayerId, CharacterId = c.Id });
            var playersIds = notKickedCharacters.Select(c => c.PlayerId);
            var characterIds = notKickedCharacters.Select(c => c.CharacterId);
            var players = await playerRepository.GetPlayers(0, playersIds.Count(), p => playersIds.Contains(p.Id));
            var charactersWithStats = await characterRepository.GetCharacters(c => characterIds.Contains(c.Id));
            return players.Join(charactersWithStats,
                p => p.Id,
                c => c.PlayerId,
                (p, c) => new CharacterWithName(p.FirstName + " " + p.LastName, c));
        }
    }
}
