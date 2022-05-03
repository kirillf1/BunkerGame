using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Health : CharacterEntity
    {
        public HealthType HealthType { get; set; } = HealthType.FullHealth;
       
        public Health(string description, bool isBalance, HealthType healthType = HealthType.FullHealth) : base(description, isBalance)
        {
            HealthType = healthType;
        }
        public void UpdateHealthType(HealthType healthType)
        {
            HealthType = healthType; 
        }
    }
    public enum HealthType
    {
        DeadDesease,
        Psychological,
        SpreadDisease,
        LiteDesease,
        FullHealth


    }
}
