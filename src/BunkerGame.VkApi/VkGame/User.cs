using BunkerGame.Domain.Shared;

namespace BunkerGame.VkApi.VkGame
{
    public class User
    {
        public User(long userId, PlayerId playerId)
        {
            UserId = userId;
            PlayerId = playerId;
            CharacterId = new CharacterId(Guid.NewGuid());
        }
        public CharacterId CharacterId { get; private set; }
        public void RecreateCharacterId()
        {
            CharacterId = new CharacterId(Guid.NewGuid());
        }
        public long UserId { get; }
        public PlayerId PlayerId { get; }
    }
}
