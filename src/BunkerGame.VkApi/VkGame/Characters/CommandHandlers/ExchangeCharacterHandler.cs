using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.Characters.CommandHandlers
{
    public class ExchangeCharacterHandler : CharacterCommandHandlerBase<Commands.ExchangeCharacter>,
        IRequestHandler<Commands.ExchangeAge>, IRequestHandler<Commands.ExchangeChildbearing>,
        IRequestHandler<Commands.ExchangeHealth>, IRequestHandler<Commands.ExchangeHobby>,
        IRequestHandler<Commands.ExchangeItem>, IRequestHandler<Commands.ExchangePhobia>,
        IRequestHandler<Commands.ExchangeProfession>, IRequestHandler<Commands.ExchangeSex>,
        IRequestHandler<Commands.ExchangeSize>, IRequestHandler<Commands.ExchangeTrait>
    {
        public ExchangeCharacterHandler(ICharacterRepository characterRepository, IEventStore eventStore) : base(characterRepository, eventStore)
        {
        }

        public override Task<Unit> Handle(Commands.ExchangeCharacter request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Unit> Handle(Commands.ExchangeAge request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Age, (a, c) => c.UpdateAge(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeChildbearing request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Childbearing, (a, c) => c.UpdateChildbearing(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeHealth request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Health, (a, c) => c.UpdateHealth(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeHobby request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Hobby, (a, c) => c.UpdateHobby(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeItem request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Health, (a, c) => c.UpdateHealth(a));
        }

        public async Task<Unit> Handle(Commands.ExchangePhobia request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Phobia, (a, c) => c.UpdatePhobia(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeProfession request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Profession, (a, c) => c.UpdateProfession(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeSex request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Sex, (a, c) => c.UpdateSex(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeSize request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Size, (a, c) => c.UpdateSize(a));
        }

        public async Task<Unit> Handle(Commands.ExchangeTrait request, CancellationToken cancellationToken)
        {
            return await Exchange(request.CharacterFirstId, request.CharacterSecondId, c => c.Trait, (a, c) => c.UpdateTrait(a));
        }

        private async Task<Unit> Exchange<T>(CharacterId characterIdFirst, CharacterId characterIdSecond, Func<Character, T> get, Action<T, Character> change)
        {
            var characterFirst = await characterRepository.GetCharacter(characterIdFirst);
            var characterSecond = await characterRepository.GetCharacter(characterIdSecond);
            var componentFirst = get(characterFirst);
            var componentSecond = get(characterSecond);
            change(componentSecond, characterFirst);
            change(componentFirst, characterSecond);
            await SaveChanges(characterFirst, characterSecond);
            return Unit.Value;
        }
        private async Task SaveChanges(Character characterFirst, Character characterSecond)
        {
            await eventStore.Save(characterFirst);
            await eventStore.Save(characterSecond);
        }
    }
}
