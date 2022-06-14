using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public class BunkerEnviroment : BunkerComponentEntity
    {
        public BunkerEnviroment(double value, string description, EnviromentBehavior enviromentBehavior = EnviromentBehavior.Unknown, 
            EnviromentType enviromentType = EnviromentType.Unknown) : base(value, description)
        {
            EnviromentType = enviromentType;
            EnviromentBehavior = enviromentBehavior;
        }
        public EnviromentBehavior EnviromentBehavior { get; private set; }
        public EnviromentType EnviromentType { get; private set; }
        public void UpadteEnviromentBehavior(EnviromentBehavior enviromentBehavior)
        {
            EnviromentBehavior = enviromentBehavior;
        }
        public void UpadteEnviromentType(EnviromentType enviromentType)
        {
            EnviromentType = enviromentType;
        }
        public override string ToString()
        {
            return $"В бункере живут: {Description}";
        }
    }
    public enum EnviromentBehavior
    {
        Agressive,
        Peaceful,
        Unknown
    }
    public enum EnviromentType
    {
        Unknown,
        Insect,
        Robot,
        Animal,
        Human,
        Chomb
    }
}
