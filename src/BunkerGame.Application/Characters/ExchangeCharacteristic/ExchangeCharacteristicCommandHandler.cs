using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Application.Characters.ExchangeCharacteristic
{
    public class ExchangeCharacteristicCommandHandler : IRequestHandler<ExchangeCharacteristicCommand, Tuple<Character, Character>>
    {
       
        private readonly ICharacterRepository characterRepository;
        public ExchangeCharacteristicCommandHandler(ICharacterRepository characterRepository)
        {
           
            this.characterRepository = characterRepository;
        }
        public async Task<Tuple<Character, Character>> Handle(
            ExchangeCharacteristicCommand command,
            CancellationToken cancellationToken)
        {
            var characterFirst = await characterRepository.GetCharacterById(command.CharacterFirstId);
            var characterSecond = await characterRepository.GetCharacterById(command.CharacterSecondId);
            if (characterFirst == null || characterSecond == null)
                throw new ArgumentNullException(nameof(characterFirst));
            string text = string.Empty;
            switch (command.CharacteristicType)
            {
                case Type t when t == typeof(Profession):
                    var tempProf = characterFirst.Profession;
                    characterFirst.UpdateProfession(characterSecond.Profession);
                    characterSecond.UpdateProfession(tempProf);
                    break;
                case Type t when t == typeof(Phobia):
                    var tempPhobia = characterFirst.Phobia;
                    characterFirst.UpdatePhobia(characterSecond.Phobia);
                    characterSecond.UpdatePhobia(tempPhobia);
                    break;

                case Type t when t == typeof(Health):
                    var tempHealth = characterFirst.Health;
                    characterFirst.UpdateHealth(characterSecond.Health);
                    characterSecond.UpdateHealth(tempHealth);
                    break;
                case Type t when t == typeof(CharacterItem):
                    var tempCharacterItem = characterFirst.CharacterItems.First(c=>c.Id!= characterFirst.Profession?.CharacterItem?.Id);
                    characterFirst.UpdateCharacterItem(characterSecond.CharacterItems.First(c => c.Id != characterFirst.Profession?.CharacterItem?.Id));
                    characterSecond.UpdateCharacterItem(tempCharacterItem);
                    break;
                case Type t when t == typeof(Childbearing):
                    var childbearingTemp = characterFirst.Childbearing;
                    characterFirst.UpdateChildbearing(characterSecond.Childbearing);
                    characterSecond.UpdateChildbearing(childbearingTemp);
                    break;
                case Type t when t == typeof(Age):
                    var ageTemp = characterFirst.Age;
                    characterFirst.UpdateAge(characterSecond.Age);
                    characterSecond.UpdateAge(ageTemp);
                    break;
                case Type t when t == typeof(AdditionalInformation):
                    var addInfTemp = characterFirst.AdditionalInformation;
                    characterFirst.UpdateAdditionalInf(characterSecond.AdditionalInformation);
                    characterSecond.UpdateAdditionalInf(addInfTemp);
                    break;
                case Type t when t == typeof(Trait):
                    var traitTemp = characterFirst.Trait;
                    characterFirst.UpdateTrait(characterSecond.Trait);
                    characterSecond.UpdateTrait(traitTemp);
                    break;
                case Type t when t == typeof(Sex):
                    var sexTemp = characterFirst.Sex;
                    characterFirst.UpdateSex(characterSecond.Sex);
                    characterSecond.UpdateSex(sexTemp);
                    break;
                case Type t when t == typeof(Size):
                    var sizeTemp = characterFirst.Size;
                    characterFirst.UpdateSize(characterSecond.Size);
                    characterSecond.UpdateSize(sizeTemp);
                    break;
                case Type t when t == typeof(Hobby):
                    var hobbyTemp = characterFirst.Hobby;
                    characterFirst.UpdateHobby(characterSecond.Hobby);
                    characterSecond.UpdateHobby(hobbyTemp);
                    break;
            }
            
            await characterRepository.CommitChanges();
            return new Tuple<Character, Character>(characterFirst, characterSecond);
        }

        
    }
}
