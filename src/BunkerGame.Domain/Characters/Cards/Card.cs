namespace BunkerGame.Domain.Characters.Cards
{
    public class Card : Value<Card>
    {
#pragma warning disable CS8618 
        private Card() { }
#pragma warning restore CS8618 
        
        public Card(string description, CardMethod cardMethod, bool fromProfession)
        {
            Description = description;
            CardMethod = cardMethod;
            FromProfession = fromProfession;
        }
        public string Description { get; }
        public CardMethod CardMethod { get; }
        public bool FromProfession { get; }

    }
}
