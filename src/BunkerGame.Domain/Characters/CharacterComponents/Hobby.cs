using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Hobby : CharacterEntity
    { 
        public HobbyType HobbyType { get; set; } = HobbyType.Useless;  
        public Hobby(string description, bool isBalance, HobbyType hobbyType = HobbyType.Useless) : base(description, isBalance)
        {
            HobbyType = hobbyType;
        }
    }
    public enum HobbyType
    {
        Surviving,
        Entertainment,
        Driving,
        Healing,
        Cooking,
        Repairing,
        HealingPhobia,
        Shooting,
        Planting,
        AnimalBreeding,
        Programming,
        Communication,
        GatherFood,
        Useless
    }
}
