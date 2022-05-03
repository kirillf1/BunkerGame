using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Phobia : CharacterEntity
    {
        public PhobiaDebuffType PhobiaDebuffType { get; set; } = PhobiaDebuffType.None;
        
        public Phobia(string description, bool isBalance, PhobiaDebuffType phobiaDebuffType = default) : base(description, isBalance)
        {
            PhobiaDebuffType = phobiaDebuffType;
        }
    }
    public enum PhobiaDebuffType
    {
        Surviving,
        Entertainment,
        None
    }
}
