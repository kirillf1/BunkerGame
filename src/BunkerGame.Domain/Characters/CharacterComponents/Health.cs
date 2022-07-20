using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Health : CharacterComponent<Health>
    {
        private Health() { }
        public static readonly Health DefaultHealth = new Health("unknown", 0, HealthType.FullHealth);
        public Health(string description, double value, HealthType healthType) : base(description, value)
        {
            HealthType = healthType;
        }
        public HealthType HealthType { get; }
    }
}
