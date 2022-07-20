namespace BunkerGame.Domain.GameSessions
{
    public class CharacterGame : Entity<CharacterId>
    {
        private CharacterGame() { }
        public CharacterGame(CharacterId id, PlayerId playerId) : base(id)
        {
            PlayerId = playerId;
            IsKicked = false;
        }
        public PlayerId PlayerId { get; }
        public byte CharacterNumber { get; private set; }
        public bool IsKicked { get; private set; }
        public void SetCharacterNumber(byte number)
        {
            CharacterNumber = number;
        }
        public void Kick()
        {
            IsKicked = true;
        }
    }
}
