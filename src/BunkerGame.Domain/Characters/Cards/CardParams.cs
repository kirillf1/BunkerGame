namespace BunkerGame.Domain.Characters.Cards
{
    public record CardParams : Value<CardParams>
    {
        public CardParams(CharacterId cardUserId, GameSessionId gameSessionId, CharacterId? targetCharacter = null)
        {
            CardUserId = cardUserId;
            GameSessionId = gameSessionId;
            TargetCharacter = targetCharacter;
        }
        public CharacterId CardUserId { get; }
        public GameSessionId GameSessionId { get; }
        public CharacterId? TargetCharacter { get; }
    }
}
