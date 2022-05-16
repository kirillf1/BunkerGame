using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;

namespace BunkerGame.Application.Characters.SpyCharacterComponent
{
    public class SpyCharacterComponentCommandHandler : IRequestHandler<SpyCharacterComponentCommand, CharacterComponent>
    {
        private readonly ICharacterRepository characterRepository;

        public SpyCharacterComponentCommandHandler(ICharacterRepository characterRepository)
        {
            this.characterRepository = characterRepository;
        }
        public async Task<CharacterComponent> Handle(SpyCharacterComponentCommand request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacterById(request.CharacterId);
            if (character == null)
                throw new ArgumentNullException(nameof(character));
            return request.MethodDirection switch
            {
                MethodDirection.AdditionalInformation => character.AdditionalInformation,
                MethodDirection.Health => character.Health,
                MethodDirection.Profession => character.Profession,
                MethodDirection.Phobia => character.Phobia,
                MethodDirection.Sex => character.Sex,
                MethodDirection.Size => character.Size,
                MethodDirection.Trait => character.Trait,
                MethodDirection.Hobby => character.Hobby,
                MethodDirection.Age => character.Age,
                MethodDirection.CharacterItem => character.CharacterItems.First(),
                MethodDirection.Childbearing => character.Childbearing,
                _ => throw new ArgumentException(nameof(request.MethodDirection) + " must contains characterComponent"),
            };
        }
    }
}
