using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public class BunkerObject : BunkerComponentEntity
    {
        public BunkerObject(double value, string description,BunkerObjectType bunkerObjectType = BunkerObjectType.Useless) : base(value, description)
        {
            BunkerObjectType = bunkerObjectType;
        }
        private List<Bunker> Bunkers = new List<Bunker>();
        public BunkerObjectType BunkerObjectType { get; private set; } = BunkerObjectType.Useless;
        public void UpdateType(BunkerObjectType bunkerObjectType)
        {
            BunkerObjectType = bunkerObjectType;
        }
        public override string ToString()
        {
            return Description;
        }
    }
    public enum BunkerObjectType
    {
        HealPlace,
        Entertainment,
        CombatPotential,
        Transport,
        PlantPlace,
        Surviving,
        AnimalBreeding,
        ToolPlace,
        Education,
        Useless
    }
}
