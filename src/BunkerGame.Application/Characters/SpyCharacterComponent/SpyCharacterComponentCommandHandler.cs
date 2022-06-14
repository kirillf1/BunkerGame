using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Application.Characters.SpyCharacterComponent
{

    public abstract class SpyCharacterComponentCommandHandler<T> : IRequestHandler<SpyCharacterComponentCommand<T>, T> where T : CharacterComponent
    {
        protected readonly ICharacterRepository characterRepository;
        protected readonly IMediator mediator;

        protected SpyCharacterComponentCommandHandler(ICharacterRepository characterRepository,IMediator mediator)
        {
            this.characterRepository = characterRepository;
            this.mediator = mediator;
        }
        public async Task<Character> GetCharacterAsync(int characterId)
        {
            var character = await characterRepository.GetCharacterById(characterId);
            if (character == null)
                throw new ArgumentNullException(nameof(character));
            return character;
        }
        protected async Task Notify(T component,Character character)
        {
            await mediator.Publish(new SpiedCharacterComponentNotification<T>(character.Id,character.GameSessionId.GetValueOrDefault(),component));
        }
        public abstract Task<T> Handle(SpyCharacterComponentCommand<T> request, CancellationToken cancellationToken);
    }
}
