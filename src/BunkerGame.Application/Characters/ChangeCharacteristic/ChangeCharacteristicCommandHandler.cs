
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Application.Characters.ChangeCharacteristic
{

    public class ChangeCharacteristicCommandHandler<T> : IRequestHandler<ChangeCharacteristicCommand<T>, Character> where T : CharacterComponent
    {
       protected readonly ICharacterComponentRepository<T> characterComponentRepository;
       protected readonly ICharacterRepository characterRepository;
       protected readonly IMediator mediator;

        public ChangeCharacteristicCommandHandler(ICharacterComponentRepository<T> characterComponentRepository, ICharacterRepository characterRepository
            ,IMediator mediator)
        {
            this.characterComponentRepository = characterComponentRepository;
            this.characterRepository = characterRepository;
            this.mediator = mediator;
        }
        public virtual async Task<Character> Handle(ChangeCharacteristicCommand<T> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacter(request.CharacterId);
            await request.ChangeMethod.Invoke(character,request.CharacterComponentId,characterComponentRepository);
            await SaveChanges();
            await Notify(character);
            return character;
        }
        protected async Task Notify(Character character)
        {
            await mediator.Publish(new CharacterUpdatedNotificationMessage(character));
        }
        protected async Task<Character> GetCharacter(int characterId)
        {
            var character = await characterRepository.GetCharacterById(characterId);
            if (character == null)
                throw new ArgumentNullException(nameof(character));
            return character;
        }
        protected async Task SaveChanges()
        {
            await characterRepository.CommitChanges();
        }
        
    }
}
