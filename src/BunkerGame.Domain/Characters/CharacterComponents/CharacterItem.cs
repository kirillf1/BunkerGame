using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class CharacterItem : CharacterEntity
    {
        // возможно добавить тип предмета
        public CharacterItemType CharacterItemType { get; set; }
        //// to 5
        //public double Value { get; set; } = 5;
        private List<Character> Characters { get; set; } = new List<Character>();

        //public List<Profession> Professions { get; set; }

        //public List<Character> Characters { get; set; }
        //public override string ToString()
        //{
        //    return Description;
        //}
        public CharacterItem(string description, bool isBalance, CharacterItemType characterItemType = CharacterItemType.None) 
            : base(description, isBalance)
        {
            CharacterItemType = characterItemType;
        }
        public void UpdateCharacterItemType(CharacterItemType characterItemType)
        {
            CharacterItemType = characterItemType;
        }
    }
    public enum CharacterItemType
    {
        Seeds,
        Food,
        Entertainment,
        Surviving,
        Tools,
        Medicines,
        Gun,
        Communication,
        KillEnviroment,
        Hunting,
        None

    }
}
