using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Trait : CharacterEntity
    {
        public TraitType TraitType { get; set; } = TraitType.Negative;
        //// to 5
        //public double Value { get; set; }
        //public List<Character> Characters { get; set; }
        //public override string ToString()
        //{
        //    return "Черта характера: " + Description;
        //}
        public Trait(string description, bool isBalance, TraitType traitType = TraitType.Negative) : base(description, isBalance)
        {
            TraitType = traitType;
        }
    }
    public enum TraitType
    {
        Surviving,
        Entertainment,
        Negative
    }
}
