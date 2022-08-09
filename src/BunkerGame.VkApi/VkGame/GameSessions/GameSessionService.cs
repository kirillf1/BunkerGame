using BunkerGame.Domain;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using BunkerGameComponents.Domain;
using MediatR;
using IUnitOfWork = BunkerGame.Domain.IUnitOfWork;

namespace BunkerGame.VkApi.VkGame.GameSessions
{
    public class GameSessionService : IApplicationService
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;

        public GameSessionService(IMediator mediator, IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(IRequest request)
        {
            if (!IsGameCommand(request))
                return;
            await mediator.Send(request);
            await unitOfWork.Save(default);
        }
        private static bool IsGameCommand(IRequest request)
        {
            return typeof(Commands).GetNestedType(request.GetType().Name) != null;
        }
    }
}
