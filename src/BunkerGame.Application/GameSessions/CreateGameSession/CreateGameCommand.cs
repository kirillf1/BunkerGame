using BunkerGame.Domain.GameSessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.CreateGameSession
{
    public class CreateGameCommand: IRequest<GameSession>
    {
        public CreateGameCommand(bool charactersAlive,byte playersCount, string gameName, long? gameId)
        {
            CharactersAlive = charactersAlive;
            PlayersCount = playersCount;
            GameName = gameName;
            GameId = gameId;
           
           
        }

        public bool CharactersAlive { get; }
        public byte PlayersCount { get; }
        public string GameName { get; }
        public long? GameId { get; }
    }
}
