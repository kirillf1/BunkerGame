using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.GameSessions;
using MediatR;

namespace BunkerGame.Application.GameSessions.ChangeCatastophe
{
    public class ChangeCatastropheCommandHandler : IRequestHandler<ChangeCatastropheCommand, Catastrophe>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly ICatastropheRepository catastropheRepository;

        public ChangeCatastropheCommandHandler(IGameSessionRepository gameSessionRepository,ICatastropheRepository catastropheRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.catastropheRepository = catastropheRepository;
        }
        public async Task<Catastrophe> Handle(ChangeCatastropheCommand command, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSessionWithCatastrophe(command.GameSessionId);
            if(gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            var catastrophe = await catastropheRepository.GetRandomCatastrophe(c=>c.Id != gameSession.Catastrophe.Id);
            gameSession.UpdateСatastrophe(catastrophe);
            await gameSessionRepository.CommitChanges();
            return catastrophe;
        }
        
    }
}
