using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.GameSessionsFactories
{
    public class GameSessionFactoryBase : IGameSessionFactory
    {
        private readonly ICharacterFactory characterFactory;
        private readonly IBunkerFactory bunkerFactory;
        private readonly ICatastropheRepository catastropheRepository;

        public GameSessionFactoryBase(ICharacterFactory characterFactory, IBunkerFactory bunkerFactory, ICatastropheRepository catastropheRepository)
        {
            this.characterFactory = characterFactory;
            this.bunkerFactory = bunkerFactory;
            this.catastropheRepository = catastropheRepository;
        }



        public async Task<GameSession> CreateGameSession(GameSessionCreateOptions gameSessionOptions)
        {
            var gameSession = new GameSession(gameSessionOptions.GameId.GetValueOrDefault(), gameSessionOptions.GameName, await CreateBunker(),
                await catastropheRepository.GetRandomCatastrophe(),
                new List<Character>()
                );
            gameSession.AddCharactersInGame(await CreateCharacters(gameSessionOptions.CharactersCount, gameSessionOptions.CharactersAlive));
            return gameSession;
        }
        private async Task<IEnumerable<Character>> CreateCharacters(byte count,bool isAlive)
        {
            return await characterFactory.CreateCharacters(count, new CharacterOptions() { FullHealthChance = 60, NotPhobiaChance = 60, IsAlive = isAlive });

        }
        private async Task<Bunker> CreateBunker()
        {
            return await bunkerFactory.CreateBunker(new BunkerCreateOptions());
        }
    }
}
