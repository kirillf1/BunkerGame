using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Profession : CharacterEntity
    {
       
        public Card? Card { get; private set; }
        public ProfessionSkill ProfessionSkill { get; private set; } = ProfessionSkill.None;
        public ProfessionType ProfessionType { get; private set; } = ProfessionType.Unknown; 
        public CharacterItem? CharacterItem { get; private set; }
       
        public Profession(string description, bool isBalance, ProfessionSkill professionSkill = ProfessionSkill.None, 
            ProfessionType professionType= ProfessionType.Unknown) : base(description, isBalance)
        {
            ProfessionSkill = professionSkill;
            ProfessionType = professionType;
        }
        public void RemoveCharacterItem()
        {
            CharacterItem = default;
        }
        public void RemoveCard()
        {
            Card = default;
        }
        public void UpdateCharacterItem(CharacterItem characterItem)
        {
            CharacterItem = characterItem ?? throw new ArgumentNullException(nameof(characterItem));
        }
        public void UpdateCard(Card card)
        {
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }
    }
    public enum ProfessionSkill
    {
        Driving,
        Healing,
        Cooking,
        Repairing,
        HealingPhobia,
        Shooting,
        Planting,
        AnimalBreeding,
        Hunting,
        GatherFood,
        Programming,
        Communication,
        None
    }
    public enum ProfessionType
    {
        Entertaining,
        Scientific,
        Surviving,
        Unknown
    }
}
