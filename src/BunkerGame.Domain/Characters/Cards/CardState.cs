namespace BunkerGame.Domain.Characters.Cards
{
    public class CardState : Entity<CardStateId>
    {    
        public CardState(CardStateId id, Card card) : base(id)
        {
            Card = card;
            IsUsed = false;
        }
        public bool IsUsed { get; private set; }
        public Card Card { get; }
        public void CardUsed()
        {
            IsUsed = true;
        }
    }
    public class CardStateId : Value<CardStateId>
    {
        public byte Value { get; }
        public CardStateId(byte value)
        {
            Value = value;
        }
    }
}
