using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents.Cards
{
    public class CardMethod
    {
        public CardMethod(MethodType methodType = MethodType.None, MethodDirection methodDirection = MethodDirection.None,
            int? itemId = null)
        {
            MethodType = methodType;
            MethodDirection = methodDirection;
            ItemId = itemId;
        }
        public int CardId { get; private set; }
        public MethodType MethodType { get; set; }
        public MethodDirection MethodDirection { get; set; }
        public int? ItemId { get; set; }
        public DirectionGroup DefineDirectionGroup()
        {
            return this.MethodDirection switch
            {
                MethodDirection.None => DirectionGroup.Unknown,
                MethodDirection.Character => DirectionGroup.Character,
                MethodDirection.Hobby => DirectionGroup.Character,
                MethodDirection.Size => DirectionGroup.Character,
                MethodDirection.AdditionalInformation => DirectionGroup.Character,
                MethodDirection.Health => DirectionGroup.Character,
                MethodDirection.Profession => DirectionGroup.Character,
                MethodDirection.Phobia => DirectionGroup.Character,
                MethodDirection.Sex => DirectionGroup.Character,
                MethodDirection.Trait => DirectionGroup.Character,
                MethodDirection.Age => DirectionGroup.Character,
                MethodDirection.CharacterItem => DirectionGroup.Character,
                MethodDirection.Childbearing => DirectionGroup.Character,
                MethodDirection.Bunker => DirectionGroup.Bunker,
                MethodDirection.BunkerWall => DirectionGroup.Bunker,
                MethodDirection.BunkerSize => DirectionGroup.Bunker,
                MethodDirection.Supplies => DirectionGroup.Bunker,
                MethodDirection.BunkerObject => DirectionGroup.Bunker,
                MethodDirection.ItemBunker => DirectionGroup.Bunker,
                MethodDirection.BunkerEnviroment => DirectionGroup.Bunker,
                MethodDirection.Catastrophe => DirectionGroup.Catastrophe,
                MethodDirection.ExternalSurrounding => DirectionGroup.GameSession,
                MethodDirection.FreePlace => DirectionGroup.GameSession,
                _ => DirectionGroup.Unknown,
            };
        }
    }
    public enum DirectionGroup
    {
        Unknown,
        Character,
        Bunker,
        Catastrophe,
        GameSession
    }
    public enum MethodType
    {
        None,
        /// <summary>
        /// self
        /// </summary>
        Update,
        /// <summary>
        /// target
        /// </summary>
        Change,
        Add,
        Exchange,
        Remove,
        PassiveRemove,
        /// <summary>
        /// target
        /// </summary>
        Spy,
        /// <summary>
        /// on yourself
        /// </summary>
        SpyYourself
    }
    public enum MethodDirection
    {
        None,
        Character,
        AdditionalInformation,
        Health,
        Profession,
        Phobia,
        Sex,
        Size,
        Trait,
        Hobby,
        Age,
        CharacterItem,
        Childbearing,
        Bunker,
        BunkerWall,
        BunkerSize,
        Supplies,
        ItemBunker,
        BunkerObject,
        BunkerEnviroment,
        Catastrophe,
        ExternalSurrounding,
        FreePlace
        
    }
}
