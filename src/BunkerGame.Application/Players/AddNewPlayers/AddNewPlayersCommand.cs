using BunkerGame.Domain.Players;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Players.AddNewPlayers
{
    public class AddNewPlayersCommand : IRequest<Unit>
    {
       public IEnumerable<Player> Players { get; }

        public AddNewPlayersCommand(IEnumerable<Player> players)
        {
            Players = players;
        }
    }
}
