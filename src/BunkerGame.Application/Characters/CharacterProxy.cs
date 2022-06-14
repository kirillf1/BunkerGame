using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters
{
    public interface ICharacterComponent<T> where T : CharacterComponent
    {
        T Component { get; set; }
    }
    public interface ICharacterComponentCollection<T> where T : CharacterComponent
    {
        IReadOnlyCollection<T> Components { get; set; }
    }
    
    public class CharacterProxy : ICharacterComponent<Phobia>,ICharacterComponentCollection<Card>,
        ICharacterComponent<Hobby>, ICharacterComponent<Sex>, ICharacterComponent<Age>, ICharacterComponent<Profession>,
        ICharacterComponent<Childbearing>, ICharacterComponent<Health>, ICharacterComponent<CharacterItem>,
        ICharacterComponent<AdditionalInformation>, ICharacterComponent<Size>, ICharacterComponent<Trait>
    {
        public readonly Character Character;

        public CharacterProxy(Character character)
        {
            this.Character = character;
        }

        Phobia ICharacterComponent<Phobia>.Component { get => Character.Phobia; set => Character.UpdatePhobia(value); }
        IReadOnlyCollection<Card> ICharacterComponentCollection<Card>.Components { get => Character.Cards; set => Character.UpdateCards(value); }
        Hobby ICharacterComponent<Hobby>.Component { get => Character.Hobby; set => Character.UpdateHobby(value); }
        Sex ICharacterComponent<Sex>.Component { get => Character.Sex; set => Character.UpdateSex(value); }
        Age ICharacterComponent<Age>.Component { get => Character.Age; set => Character.UpdateAge(value); }
        Profession ICharacterComponent<Profession>.Component { get => Character.Profession; set => Character.UpdateProfession(value); }
        Childbearing ICharacterComponent<Childbearing>.Component { get => Character.Childbearing; 
            set => Character.UpdateChildbearing(value); }
        Health ICharacterComponent<Health>.Component { get => Character.Health; set => Character.UpdateHealth(value); }
        CharacterItem ICharacterComponent<CharacterItem>.Component { get => Character.CharacterItems.First();
            set => Character.UpdateCharacterItem(value); }
        AdditionalInformation ICharacterComponent<AdditionalInformation>.Component { get => Character.AdditionalInformation; set => Character.UpdateAdditionalInf(value); }
        Size ICharacterComponent<Size>.Component { get => Character.Size; set => Character.UpdateSize(value); }
        Trait ICharacterComponent<Trait>.Component { get => Character.Trait; set => Character.UpdateTrait(value); }

        public bool IsContainsCharacterComponent<T>() where T : CharacterComponent
        {
            return this is ICharacterComponent<T>;
        }
        public bool IsContainsCharacterComponentCollection<T>() where T : CharacterComponent
        {
            return this is ICharacterComponentCollection<T>;
        }
        public ICharacterComponent<T> GetCharacterComponent<T>() where T : CharacterComponent
        {
            return this as ICharacterComponent<T> ?? 
                throw new NotImplementedException($"Character does not contain a property {typeof(T).Name}");
            
        }
        public ICharacterComponentCollection<T> GetCharacterComponentCollection<T>() where T : CharacterComponent
        {
            return this as ICharacterComponentCollection<T> ??
                throw new NotImplementedException($"Character does not contain a property {typeof(T).Name}");

        }
    }
}
