
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Application.Characters.ChangeCharacteristic
{
    public class ChangeCharacteristicCommandHandler : IRequestHandler<ChangeCharacteristicCommand, Character>
    {
        private readonly ICharacterComponentRepLocator charcterComponentRep;
        private readonly ICharacterRepository characterRepository;

        public ChangeCharacteristicCommandHandler(ICharacterComponentRepLocator charcterComponentRep, ICharacterRepository characterRepository)
        {
            this.charcterComponentRep = charcterComponentRep;
            this.characterRepository = characterRepository;
        }
        public async Task<Character> Handle(ChangeCharacteristicCommand request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacterById(request.CharacterId);
            if (character == null)
                throw new ArgumentNullException(nameof(character));
            string text = string.Empty;
            int? targetComponentId = request.CharacteristicId;
            switch (request.CharacteristicType)
            {
                case Type t when t == typeof(Profession):
                    var profession = await charcterComponentRep.GetCharacterComponent<Profession>(true,
                        p => targetComponentId.HasValue ? p.Id == targetComponentId : p.Id != character.Profession.Id);
                    character.UpdateProfession(profession);
                    break;
                case Type t when t == typeof(Phobia):
                    var phobia = await charcterComponentRep.GetCharacterComponent<Phobia>(true
                        , targetComponentId.HasValue ? p => p.Id == targetComponentId : null);
                    character.UpdatePhobia(phobia);
                    break;

                case Type t when t == typeof(Health):
                    var health = await charcterComponentRep.GetCharacterComponent<Health>(true,
                         targetComponentId.HasValue ? p => p.Id == targetComponentId : null);
                    character.UpdateHealth(health);
                    break;

                case Type t when t == typeof(CharacterItem):
                    var characterItem = await charcterComponentRep.GetCharacterComponent<CharacterItem>(true,
                        c => targetComponentId.HasValue ? c.Id == targetComponentId : character.CharacterItems.Any(i => i.Id != c.Id));
                    character.UpdateCharacterItem(characterItem);
                    break;
                case Type t when t == typeof(Childbearing):
                    var childbearing = targetComponentId.HasValue ? new Childbearing(targetComponentId == 1) 
                        : await charcterComponentRep.GetCharacterComponent<Childbearing>(true);
                    character.UpdateChildbearing(childbearing);
                    break;
                case Type t when t == typeof(Age):

                    var age = targetComponentId.HasValue ? new Age(targetComponentId.Value) : await charcterComponentRep.GetCharacterComponent<Age>(true);
                    character.UpdateAge(age);
                    break;
                case Type t when t == typeof(AdditionalInformation):
                    var addInf = await charcterComponentRep.GetCharacterComponent<AdditionalInformation>(true,
                        p => targetComponentId.HasValue ? p.Id == targetComponentId : p.Id != character.AdditionalInformation.Id);
                    character.UpdateAdditionalInf(addInf);
                    break;
                case Type t when t == typeof(Trait):
                    var trait = await charcterComponentRep.GetCharacterComponent<Trait>(true,
                        c => targetComponentId.HasValue ? c.Id == targetComponentId : character.Trait.Id != c.Id);
                    character.UpdateTrait(trait);
                    break;
                case Type t when t == typeof(Sex): 
                    var sex = targetComponentId.HasValue
                        ? new Sex(targetComponentId.Value == 1)
                        : await charcterComponentRep.GetCharacterComponent<Sex>(true);
                    character.UpdateSex(sex);
                    break;
                case Type t when t == typeof(Size):
                    var size = await charcterComponentRep.GetCharacterComponent<Size>(true);
                    character.UpdateSize(size);
                    break;
                case Type t when t == typeof(Hobby):
                    var hobby = await charcterComponentRep.GetCharacterComponent<Hobby>(true,
                        c => targetComponentId.HasValue ? c.Id == targetComponentId : character.Hobby.Id != c.Id);
                    character.UpdateHobby(hobby);
                    break;
            }
            await characterRepository.CommitChanges();
            return character;
        }
    }
}
