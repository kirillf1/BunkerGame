using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class AdditionalInformation : CharacterEntity
    {
        
        public AdditionalInformation(string description, bool isBalance, AddInfType addInfType = AddInfType.Useless) : base(description, isBalance)
        {
           AddInfType = addInfType;
        }
        public AddInfType AddInfType { get; set; }
        public void UpdateAddInfType(AddInfType addInfType)
        {
            AddInfType = addInfType;
        }
        // to 2
        

        //public List<Character> Characters { get; set; }
        //public override string ToString()
        //{
        //    return "Дополнительная информация: " + Description;
        //}
    }
    public enum AddInfType
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
        Useless
    }
}
