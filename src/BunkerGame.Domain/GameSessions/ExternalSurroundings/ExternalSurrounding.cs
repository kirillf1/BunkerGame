using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions.ExternalSurroundings
{
    public class ExternalSurrounding
    {
        public ExternalSurrounding(string description, double value = 0, SurroundingType surroundingType = SurroundingType.Unknown)
        {
            Description = description;
            Value = value;
            SurroundingType = surroundingType;
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public SurroundingType SurroundingType { get; set; }
        private List<GameSession> GameSessions = new();
    }
    public enum SurroundingType
    {
        Unknown,
        AgressivePeople,
        Guns,
        Food,
        PlantPlace,
        PeacefulWomen,
        PeacefulMen,
        AgressiveCreatures
    }
}
