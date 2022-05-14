using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Configuration.TextConventers
{
    public static class GameComponentTypeTextConventer
    {
        public static Type? ConvertTextToBunkerComponentTypeEn(string textType)
        {
            return textType.ToLower() switch
            {
                "bunkerenviroment" => typeof(BunkerEnviroment),
                "bunkerobject" => typeof(BunkerObject),
                "bunkerwall" => typeof(BunkerWall),
                "itembunker" => typeof(ItemBunker),
                "bunkersize" => typeof(BunkerSize),
                "supplies" => typeof(Supplies),
                _ => null,
            };
        }
        public static Type? ConvertTextToCharacteristicTypeEn(string textType)
        {
            return textType.ToLower() switch
            {
                "profession" => typeof(Profession),
                "phobia" => typeof(Phobia),
                "health" => typeof(Health),
                "characteritem" => typeof(CharacterItem),
                "childbearing" => typeof(Childbearing),
                "age" => typeof(Age),
                "additionalinformation" => typeof(AdditionalInformation),
                "trait" => typeof(Trait),
                "sex" => typeof(Sex),
                "size" => typeof(Size),
                "hobby" => typeof(Hobby),
                _ => null,
            };
        }
        public static Type? ConvertTextToCharacteristicTypeRussian(string textType)
        {
            return textType.ToLower() switch
            {
                "профессия" => typeof(Profession),
                "фобия" => typeof(Phobia),
                "здоровье" => typeof(Health),
                "багаж" => typeof(CharacterItem),
                "деторождение" => typeof(Childbearing),
                "возраст" => typeof(Age),
                "доп.информация" => typeof(AdditionalInformation),
                "черта характера" => typeof(Trait),
                "пол" => typeof(Sex),
                "масса и рост" => typeof(Size),
                "хобби" => typeof(Hobby),
                _ => null,
            };
        }
    }
}
