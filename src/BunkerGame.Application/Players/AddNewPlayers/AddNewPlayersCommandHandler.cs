using BunkerGame.Domain.Players;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Players.AddNewPlayers
{
    public class AddNewPlayersCommandHandler : IRequestHandler<AddNewPlayersCommand, Unit>
    {
        private readonly IPlayerRepository playerRepository;

        public AddNewPlayersCommandHandler(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }
        public async Task<Unit> Handle(AddNewPlayersCommand request, CancellationToken cancellationToken)
        {
            var newPlayers = request.Players.Where(p => p.Id == 0).ToList();
            var playersForAdd = request.Players;
            if (newPlayers.Any())
            {
                newPlayers.ForEach(async c => await playerRepository.AddPlayer(c));
                playersForAdd = playersForAdd.DistinctBy(c => newPlayers.Any(p => p.Id == c.Id));
            }
            var notExistingPlayerIds = await playerRepository.GetNoExistingIds(playersForAdd.Select(c => c.Id));
            foreach (var player in playersForAdd)
            {
                if (notExistingPlayerIds.Contains(player.Id))
                    await playerRepository.AddPlayer(player);

            }
            await playerRepository.CommitChanges();
            return Unit.Value;
        }
    }
}
