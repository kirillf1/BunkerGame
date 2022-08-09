using BunkerGame.Domain;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using BunkerGame.Framework;
using BunkerGame.VkApi.VkGame.Characters.CommandHandlers;
using BunkerGameComponents.Domain;
using MediatR;
using IUnitOfWork = BunkerGame.Domain.IUnitOfWork;

namespace BunkerGame.VkApi.VkGame.Characters
{
    public class CharacterService : IApplicationService
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        public CharacterService(IMediator mediator, IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(IRequest request)
        {
            if (!IsCharacterCommand(request))
                return;
            await mediator.Send(request);
            await unitOfWork.Save(default);
        }
        private static bool IsCharacterCommand(IRequest request)
        {
            return typeof(Commands).GetNestedType(request.GetType().Name) != null;
        }
    }
}
