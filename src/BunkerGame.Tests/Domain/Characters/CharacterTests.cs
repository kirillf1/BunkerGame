using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using BunkerGame.GameTypes.CharacterTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Domain.Characters
{
    public class CharacterTests
    {
        [Fact]
        public void UseCard_InvalidCardNumber_InvokeArgumentOutOfRangeException()
        {
            var character = CreateCharacterWithRandomTwoCards();

            Assert.Throws<ArgumentOutOfRangeException>(() => character.UseCard(4, null));
        }
        [Theory]
        [InlineData(MethodType.Change,MethodDirection.Bunker,typeof( BunkerGame.Domain.GameSessions.Commands.UpdateToRandomBunker))]
        [InlineData(MethodType.Change, MethodDirection.BunkerObject, typeof(BunkerGame.Domain.GameSessions.Commands.UpdateBunkerBuildings))]
        [InlineData(MethodType.Change, MethodDirection.BunkerWall, typeof(BunkerGame.Domain.GameSessions.Commands.UpdateBunkerCondition))]
        [InlineData(MethodType.Update, MethodDirection.Age, typeof(Commands.UpdateAge))]
        [InlineData(MethodType.Update, MethodDirection.CharacterItem, typeof(Commands.UpdateItem))]
        [InlineData(MethodType.Update, MethodDirection.Profession, typeof(Commands.UpdateProfession))]
        [InlineData(MethodType.Spy, MethodDirection.AdditionalInformation, typeof(Commands.UncoverAdditionalInformation))]
        [InlineData(MethodType.Spy, MethodDirection.Health, typeof(Commands.UncoverHealth))]
        [InlineData(MethodType.SpyYourself, MethodDirection.Trait, typeof(Commands.UncoverTrait))]
        [InlineData(MethodType.Exchange, MethodDirection.Childbearing, typeof(Commands.ExchangeChildbearing))]
        [InlineData(MethodType.Add, MethodDirection.FreePlace, typeof(BunkerGame.Domain.GameSessions.Commands.AddSeats))]
        public void UseCard_ValidMethodDirectionAndMethodType_ValidCardResult(MethodType methodType, MethodDirection methodDirection,Type commandType)
        {
            var character = CreateCharacterWithSpecificCards(methodType, methodDirection);
            CharacterId? targetCharacterId = default;
            if (character.Cards.First().Card.CardMethod.IsTargetCharacterCard())
                targetCharacterId = new CharacterId(Guid.NewGuid());

            var commandResult = character.UseCard(1, targetCharacterId);
            var command = commandResult.GetCommand();

            Assert.True(commandResult.IsValid);
            Assert.Equal(commandType, command.GetType());
        }
        [Theory]
        [InlineData(MethodType.Spy, MethodDirection.Bunker)]
        [InlineData(MethodType.Add, MethodDirection.BunkerObject)]
        [InlineData(MethodType.Remove, MethodDirection.BunkerWall)]
        [InlineData(MethodType.Spy, MethodDirection.Childbearing)]
        [InlineData(MethodType.Exchange, MethodDirection.BunkerWall)]
        [InlineData(MethodType.Exchange, MethodDirection.Age)]
        public void UseCard_InvalidMethodTypeOrDirection_InvalidCommandResult(MethodType methodType, MethodDirection methodDirection)
        {
            var character = CreateCharacterWithSpecificCards(methodType, methodDirection);

            var commandResult = character.UseCard(1, null);

            Assert.False(commandResult.IsValid);
        }
        public static Character CreateCharacterWithSpecificCards(MethodType methodType, MethodDirection methodDirection)
        {
            var character = new Character(new CharacterId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()), new GameSessionId(Guid.NewGuid()));
            var cards = new List<BunkerGame.Domain.Characters.Cards.Card>(2);
            var card1 = new BunkerGame.Domain.Characters.Cards.Card("testCard1", new BunkerGame.Domain.Characters.Cards.CardMethod(methodType, methodDirection, null), false);
            var card2 = new BunkerGame.Domain.Characters.Cards.Card("testCard2", new BunkerGame.Domain.Characters.Cards.CardMethod(methodType, methodDirection, null), false);
            cards.Add(card1);
            cards.Add(card2);
            character.UpdateCards(cards);
            return character;
        }
        private static Character CreateCharacterWithRandomTwoCards()
        {
            var character = new Character(new CharacterId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()), new GameSessionId(Guid.NewGuid()));
            var cards = new List<BunkerGame.Domain.Characters.Cards.Card>(2);
            cards.Add(new BunkerGame.Domain.Characters.Cards.Card("testCard1", new BunkerGame.Domain.Characters.Cards.CardMethod(MethodType.SpyYourself, MethodDirection.Sex, null), false));
            cards.Add(new BunkerGame.Domain.Characters.Cards.Card("testCard2", new BunkerGame.Domain.Characters.Cards.CardMethod(MethodType.Change, MethodDirection.Bunker, null), false));
            character.UpdateCards(cards);
            return character;

        }
    }
}
