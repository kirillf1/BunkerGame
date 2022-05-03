using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.Helpers
{
    public static class CharacterCreator
    {
        static Random random;
        static CharacterCreator()
        {
            random = new Random();
        }
        public static Character CreateCharacter()
        {
            return new Character(new Profession($"Prof:{random.Next()}", true),
                new Sex(), new Age(), new Childbearing(random.Next(0, 100) > 50), new Size(), new Health($"testHealth:{random.Next()}", true),
                new Trait($"Trait:{random.Next()}", true), new Phobia($"Phobia:{random.Next()}", true), new Hobby($"Hobby:{random.Next()}", true),
                new AdditionalInformation($"AddInf:{random.Next()}", true), new CharacterItem($"CharacterItem:{random.Next()}", true),
                new List<Card> { new Card($"Card:{random.Next()}",new CardMethod(), true, false), new Card($"Card:{random.Next()}", new CardMethod(), true, false) }
                );
        }
        public static IEnumerable<Character> CreateCharacters(int count)
        {
            var listCharacter = new List<Character>();
            for (int i = 0; i < count; i++)
            {
                listCharacter.Add(CreateCharacter());
            }
            return listCharacter;
        }
    }
}
