using BunkerGame.Domain.Characters.Cards;
using BunkerGame.Domain.Characters.CharacterComponents;

namespace BunkerGame.Domain.Characters
{
    public class Character : AggregateRoot<CharacterId>
    {
        private Character()
        {

        }
        public Character(CharacterId id, PlayerId playerId, GameSessionId gameSessionId)
        {
            Id = id;
            PlayerId = playerId;
            GameSessionId = gameSessionId;
            Sex = new Sex();
            Profession = Profession.DefaultProfession;
            Health = Health.DefaultHealth;
            Hobby = Hobby.DefaultHobby;
            Childbearing = new Childbearing(false);
            Trait = Trait.DefaultTrait;
            Phobia = Phobia.DefaultPhobia;
            Size = new Size();
            cards = new(3);
            items = new(2);
            AdditionalInformation = AdditionalInformation.DefaultAdditionalInformation;
            Age = new Age();
            AddEvent(new Events.CharacterCreated(this));
        }
        public Character(CharacterId id, PlayerId playerId, GameSessionId gameSessionId,
            IEnumerable<Card> cards, IEnumerable<Item> items, Phobia phobia,
            Health health, Sex sex, AdditionalInformation addinf, Childbearing childbearing,
            Profession profession, Age age, Size size, Trait trait, Hobby hobby)
        {
            Id = id;
            PlayerId = playerId;
            GameSessionId = gameSessionId;
            this.items = new(items);
            Phobia = phobia;
            Health = health;
            Sex = sex;
            Childbearing = childbearing;
            Profession = profession;
            Age = age;
            Size = size;
            Trait = trait;
            Hobby = hobby;
            AdditionalInformation = addinf;
            this.cards = new(cards.Select((card, i) => new CardState(new CardStateId((byte)(i + 1)), card)));
            AddEvent(new Events.CharacterCreated(this));
        }
        public PlayerId PlayerId { get; }
        public GameSessionId GameSessionId { get; }
        public Profession Profession { get; private set; }
        public Sex Sex { get; private set; }
        public Age Age { get; private set; }
        public Childbearing Childbearing { get; private set; }
        public Size Size { get; private set; }
        public Health Health { get; private set; }
        public Trait Trait { get; private set; }
        public Phobia Phobia { get; private set; }
        public Hobby Hobby { get; private set; }
        public AdditionalInformation AdditionalInformation { get; private set; }
        public IReadOnlyCollection<Item> Items { get => items; } 
        private List<Item> items;
        public IReadOnlyCollection<CardState> Cards { get => cards; }
        private List<CardState> cards;
        public void UpdateSex(Sex sex)
        {
            Sex = sex ?? throw new ArgumentNullException(nameof(sex));
            AddEvent(new Events.SexUpdated(Id, Sex));
        }
        public void UpdateAge(Age age)
        {
            Age = age ?? throw new ArgumentNullException(nameof(age));
            AddEvent(new Events.AgeUpdated(Id, Age));
        }
        public void UpdateCards(IEnumerable<Card> newCards)
        {
            var cardCount = newCards.Count();
            if (cardCount > 3)
                throw new ArgumentException("Cards must be less then 3");
            var cardFromProfession = Cards.FirstOrDefault(c => c.Card.FromProfession);
            cards.Clear();
            cards.AddRange(newCards.Select((card, i) => new CardState(new CardStateId((byte)(i + 1)), card)));
            if (cardFromProfession != null)
                cards.Add(cardFromProfession);
            AddEvent(new Events.CardsUpdated(Id, Cards));
        }
        public void UpdateItem(Item characterItem)
        {
            if (characterItem.FromProfession)
                items.RemoveAll(i => i.FromProfession);
            else
                items.RemoveAll(i => !i.FromProfession);
            items.Add(characterItem);
            AddEvent(new Events.CharacterItemsUpdated(Id, Items));
        }
        public void UpdateHobby(Hobby hobby)
        {
            Hobby = hobby ?? throw new ArgumentNullException(nameof(hobby));
            AddEvent(new Events.HobbyUpdated(Id, Hobby));
        }
        public void UpdateAdditionalInf(AdditionalInformation additionalInformation)
        {
            AdditionalInformation = additionalInformation ?? throw new ArgumentNullException(nameof(additionalInformation));
            AddEvent(new Events.AdditionalInformationUpdated(Id, AdditionalInformation));
        }
        public void UpdatePhobia(Phobia phobia)
        {
            Phobia = phobia ?? throw new ArgumentNullException(nameof(phobia));
            AddEvent(new Events.PhobiaUpdated(Id, Phobia));
        }
        public void UpdateTrait(Trait trait)
        {
            Trait = trait ?? throw new ArgumentNullException(nameof(trait));
            AddEvent(new Events.TraitUpdated(Id, Trait));
        }
        public void UpdateHealth(Health health)
        {
            Health = health ?? throw new ArgumentNullException(nameof(health));
            AddEvent(new Events.HealthUpdated(Id, Health));
        }
        public void UpdateSize(Size size)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
            AddEvent(new Events.SizeUpdated(Id, Size));
        }
        public void UpdateChildbearing(Childbearing childbearing)
        {
            Childbearing = childbearing ?? throw new ArgumentNullException(nameof(childbearing));
            AddEvent(new Events.ChildbearingUpdated(Id, childbearing));
        }
        public void UpdateProfession(Profession profession)
        {
            var cardCount = Cards.Count;
            cards.RemoveAll(c => c.Card.FromProfession);
            Profession = profession;
            AddEvent(new Events.ProfessionUpdated(Id, Profession));
            if (cardCount != Cards.Count)
                AddEvent(new Events.CardsUpdated(Id, Cards));
        }
        public CardCommandResult UseCard(byte cardNumber, CharacterId? targetCharacter)
        {
            if (cardNumber > Cards.Count)
                throw new ArgumentOutOfRangeException("card number must be 1 or 3");
            var cardCommandBuilder = new CardCommandResultBuilder();
            var cardState = Cards.First(c => c.Id.Value == cardNumber);
            if (cardState.IsUsed)
            {
                cardCommandBuilder.AddError(CardExecuteError.CardUsed);
                return cardCommandBuilder.Build();
            }
            var cardMethod = cardState.Card.CardMethod;
            cardMethod.SetGetCommand(new CardParams(Id, GameSessionId, targetCharacter), cardCommandBuilder);
            var cardResult = cardCommandBuilder.Build();
            if (!cardResult.IsValid)
                return cardResult;
            cardState.CardUsed();
            AddEvent(new Events.CardUsed(Id, GameSessionId, cardState.Card, targetCharacter));
            return cardResult;
        }
    }
}
