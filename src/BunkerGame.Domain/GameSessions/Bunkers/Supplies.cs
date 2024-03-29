﻿namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public record Supplies : Value<Supplies>
    {
        private Supplies() { }
        public Supplies(int suplliesYears)
        {
            Years = suplliesYears;
        }

        public int Years { get; }
        public override string ToString()
        {
            return "Припасы на срок:" + Years;
        }
    }
}
