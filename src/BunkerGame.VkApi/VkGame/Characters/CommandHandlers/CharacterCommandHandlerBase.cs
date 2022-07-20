using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.Characters.CommandHandlers
{
    public abstract class CharacterCommandHandlerBase<T> : IRequestHandler<T> where T : IRequest
    {
        protected readonly ICharacterRepository characterRepository;
        protected readonly IEventStore eventStore;

        protected CharacterCommandHandlerBase(ICharacterRepository characterRepository, IEventStore eventStore)
        {
            this.characterRepository = characterRepository;
            this.eventStore = eventStore;
        }
        public abstract Task<Unit> Handle(T request, CancellationToken cancellationToken);
        protected async Task UpdateCharacter(CharacterId characterId, Action<Character> update)
        {
            var character = await characterRepository.GetCharacter(characterId);
            update(character);
            await eventStore.Save(character);
        }
    }
}
