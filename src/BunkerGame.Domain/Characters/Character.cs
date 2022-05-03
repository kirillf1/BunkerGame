using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using BunkerGame.Domain.GameSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters
{
    public class Character
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private Character()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {

        }
        public Character(Profession profession, Sex sex, Age age, Childbearing childbearing, Size size,
            Health health, Trait trait, Phobia phobia, Hobby hobby, AdditionalInformation additionalInformation,
            CharacterItem characterItem, IEnumerable<Card> cards)
        {
            Profession = profession ?? throw new ArgumentNullException(nameof(profession));
            Sex = sex ?? throw new ArgumentNullException(nameof(sex));
            Age = age ?? throw new ArgumentNullException(nameof(age));
            Childbearing = childbearing ?? throw new ArgumentNullException(nameof(childbearing));
            Size = size ?? throw new ArgumentNullException(nameof(size));
            Health = health ?? throw new ArgumentNullException(nameof(health));
            Trait = trait ?? throw new ArgumentNullException(nameof(trait));
            Phobia = phobia ?? throw new ArgumentNullException(nameof(phobia));
            Hobby = hobby ?? throw new ArgumentNullException(nameof(hobby));
            AdditionalInformation = additionalInformation ?? throw new ArgumentNullException(nameof(additionalInformation));
            CharacterItems = new(2);
            Cards = new(3);
            if (cards.Count() != 2)
                throw new ArgumentException("Cards must be 2");
            Cards.AddRange(cards);
            if (Profession.Card != null)
                Cards.Add(Profession.Card);
            if (characterItem == null)
                throw new ArgumentNullException(nameof(characterItem));
            CharacterItems.Add(characterItem);
            if (profession.CharacterItem != null)
                CharacterItems.Add(profession.CharacterItem);

            UsedCards = new(Cards.Select((c, n) => new UsedCard(false, Id, c.Id, (byte)(n + 1))));

        }
        public int Id { get; private set; }
        public long? PlayerId { get; private set; }

        public byte CharacterNumber { get; private set; }

        public long? GameSessionId { get; private set; }

        public Profession Profession { get; private set; }

        private GameSession? GameSession { get; set; }
        //public int ProfessionId { get; set; }
        public Sex Sex { get; private set; }
        public Age Age { get; private set; }
        public byte ExperienceProfession { get; private set; }
        public Childbearing Childbearing { get; private set; }
        public bool IsAlive { get; private set; }
        public byte ExperienceHobby { get; private set; }
        public Size Size { get; private set; }
        public Health Health { get; private set; }
        //public int HealthId { get; set; }
        public Trait Trait { get; private set; }
        //public int TraitId { get; set; }
        public Phobia Phobia { get; private set; }
        //public int PhobiaId { get; set; }
        public Hobby Hobby { get; private set; }
        //public int HobbyId { get; set; }
        public AdditionalInformation AdditionalInformation { get; private set; }
        //public int AdditionalInformationId { get; set; }
        public List<CharacterItem> CharacterItems { get; private set; } = new List<CharacterItem>();
        public List<Card> Cards { get; private set; } = new List<Card>();
        public List<UsedCard> UsedCards { get; private set; }
        public void ChangeLive(bool isAlive)
        {
            IsAlive = isAlive;
        }
        public void SetCharacterExpirience(byte hobbyExp, byte profExp)
        {
            if (profExp < 0)
                throw new ArgumentException("ExperienceProfession must be more than or equals 0");
            ExperienceProfession = profExp;
            if (hobbyExp < 0)
                throw new ArgumentException("HobbyExperience must be more than or equals 0");
            Age = new Age(Age.Count + profExp);
            ExperienceHobby = hobbyExp;
        }
        public void SetPlayerId(long id)
        {
            PlayerId = id;
        }
        public void UpdateSex(Sex sex)
        {
            Sex = sex ?? throw new ArgumentNullException(nameof(sex));
        }
        public void RegisterCharacterInGame(byte position, long gameSessionId)
        {
            if (position < 1 || position > 13)
                throw new ArgumentException("position must be 1 to 12");
            //if (gameSessionId < 0)
            //    throw new ArgumentException("GameSessionId must be more than 0");
            GameSessionId = gameSessionId;
            CharacterNumber = position;
        }
        public void UpdateAge(Age age)
        {
            Age = age ?? throw new ArgumentNullException(nameof(age));
        }
        public void UpdateCards(IEnumerable<Card> newCards)
        {
            var cardCount = newCards.Count();
            if (cardCount != 2)
                throw new ArgumentException("Cards must be 2");
            if (Profession.Card != null)
            {
                Cards.RemoveAll(c => c.Id != Profession.Card.Id);
                UsedCards.RemoveAll(c => c.CardId != Profession.Card.Id);
            }
            else
            {
                Cards.Clear();
                UsedCards.Clear();
            }
            Cards.AddRange(newCards);
            UsedCards.AddRange(newCards.Select((c, n) => new UsedCard(false, Id, c.Id, (byte)(n + 1))));
        }
        public void UpdateCharacterItem(CharacterItem characterItem)
        {
            if (characterItem is null)
            {
                throw new ArgumentNullException(nameof(characterItem));
            }
            if (Profession.CharacterItem != null)
                CharacterItems.RemoveAll(c => Profession.CharacterItem.Id != c.Id);
            else
                CharacterItems.Clear();
            CharacterItems.Add(characterItem);
        }
        public void UpdateHobby(Hobby hobby)
        {
            if (hobby is null)
            {
                throw new ArgumentNullException(nameof(hobby));
            }
            Hobby = hobby ?? throw new ArgumentNullException(nameof(hobby));
        }
        public void UpdateAdditionalInf(AdditionalInformation additionalInformation)
        {
            if (additionalInformation is null)
            {
                throw new ArgumentNullException(nameof(additionalInformation));
            }
            AdditionalInformation = additionalInformation ?? throw new ArgumentNullException(nameof(additionalInformation));
        }
        public void UpdatePhobia(Phobia phobia)
        {
            if (phobia is null)
            {
                throw new ArgumentNullException(nameof(phobia));
            }
            Phobia = phobia ?? throw new ArgumentNullException(nameof(phobia));
        }
        public void UpdateTrait(Trait trait)
        {
            Trait = trait ?? throw new ArgumentNullException(nameof(trait));
        }
        public void UpdateHealth(Health health)
        {
            Health = health ?? throw new ArgumentNullException(nameof(health));
        }
        public void UpdateSize(Size size)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
        }
        public void UpdateChildbearing(Childbearing childbearing)
        {
            Childbearing = childbearing ?? throw new ArgumentNullException(nameof(childbearing));
        }
        public void UpdateProfession(Profession profession)
        {
            if (profession is null)
            {
                throw new ArgumentNullException(nameof(profession));
            }
            if (Profession.CharacterItem != null)
            {
                CharacterItems.Remove(Profession.CharacterItem);
            }
            if (Profession.Card != null)
            {
                Cards.Remove(Profession.Card);
                UsedCards.Remove(UsedCards.First(c => c.CardId == Profession.Id));
            }
            Profession = profession;
            if (profession.Card != null)
            {
                Cards.Add(profession.Card);
                UsedCards.Add(new UsedCard(false, Id, profession.Card.Id, (byte)(Cards.Count + 1)));
            }
            if (profession.CharacterItem != null)
            {
                CharacterItems.Add(profession.CharacterItem);
            }
        }
        public Card GetCardByNumber(byte cardNumber)
        {
            if (cardNumber > UsedCards.Count || cardNumber < 1)
                throw new ArgumentOutOfRangeException("value must be 1 or 3");
            return UsedCards.Join(Cards,u=>u.CardId, c => c.Id, (u, c) => new {u.CardNumber, c}).First(c=>c.CardNumber == cardNumber).c;
        }
        public bool CheckCardUsed(byte cardNumber)
        {
            if (cardNumber > UsedCards.Count || cardNumber < 1)
                throw new ArgumentOutOfRangeException("value must be 1 or 3");
            return UsedCards.First(c => c.CardNumber == cardNumber).CardUsed;
        }
    }
}
