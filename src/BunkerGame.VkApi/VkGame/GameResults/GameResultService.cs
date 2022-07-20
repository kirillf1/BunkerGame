using BunkerGame.Domain;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameResults
{
    public class GameResultService : IApplicationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;

        public GameResultService(IUnitOfWork unitOfWork, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }
        public async Task Handle(IRequest request)
        {
            await mediator.Send(request);
            await unitOfWork.Save(default);
        }
    }
}
