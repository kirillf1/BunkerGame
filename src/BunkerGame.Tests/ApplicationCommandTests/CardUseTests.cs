using BunkerGame.Application.Characters.UserCard;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BunkerGame.Domain.Characters;
using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Bunkers.ChangeBunkerComponent;
using BunkerGame.Application.Characters.SpyCharacterComponent;

namespace BunkerGame.Tests.ApplicationCommandTests
{
    
    public class CardUseTests
    {
        [Theory]
        [InlineData(MethodDirection.Trait)]
        [InlineData(MethodDirection.AdditionalInformation)]
        [InlineData(MethodDirection.Profession)]
        [InlineData(MethodDirection.CharacterItem)]
        [InlineData(MethodDirection.Phobia)]
        [InlineData(MethodDirection.Age)]
        [InlineData(MethodDirection.Hobby)]
        [InlineData(MethodDirection.Health)]
        public async void UseChangeCharacteristicCard_NoneTarget_ShouldInvoke_ChangeCharacteristicCommand( MethodDirection methodDirection)
        {
            var character = GetCharacterWithConcreteCard(methodDirection, MethodType.Update);
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(x=> x.GetCharacterById(character.Id,true)).ReturnsAsync(character);
            var mediator = new Mock<IMediator>();
            IRequestHandler<UseCardNoneTargetCommand, Unit> cardUseCommand = 
                new UseCardNoneTargetCommandHandler(mediator.Object, characterRepository.Object);

            var result = await cardUseCommand.Handle(new UseCardNoneTargetCommand(character.Id, 1),default);
            Assert.Equal(Unit.Value, result);

            mediator.Verify(x => x.Send(It.Is<object>(c=>c.GetType() == typeof(ChangeCharacteristicCommand)),default),Times.Once);
        }
        [Theory]
        [InlineData(MethodDirection.Trait)]
        [InlineData(MethodDirection.AdditionalInformation)]
        [InlineData(MethodDirection.Profession)]
        [InlineData(MethodDirection.CharacterItem)]
        [InlineData(MethodDirection.Phobia)]
        [InlineData(MethodDirection.Age)]
        [InlineData(MethodDirection.Hobby)]
        [InlineData(MethodDirection.Health)]
        public async void UseChangeCharactericCard_Target_ShouldInvoke_UpdateCharacteristicCommand(MethodDirection methodDirection)
        {
            var characterCardUser = GetCharacterWithConcreteCard(methodDirection, MethodType.Change);
            var characterTarget = CharacterCreator.CreateCharacter();
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(x => x.GetCharacterById(characterCardUser.Id, true)).ReturnsAsync(characterCardUser);
            var mediator = new Mock<IMediator>();
            IRequestHandler<UseCardOnOtherCharacterCommand, Unit> cardUseCommand =
                new UseCardOnOtherCharacterCommandHandler(characterRepository.Object, mediator.Object);
            var result = await cardUseCommand.Handle(new UseCardOnOtherCharacterCommand( 1, characterCardUser.Id, characterTarget.Id), default);
            Assert.Equal(Unit.Value, result);

            mediator.Verify(x => x.Send(It.Is<object>(c => c.GetType() == typeof(ChangeCharacteristicCommand)), default), Times.Once);
        }
        [Theory]
        [InlineData(MethodDirection.BunkerEnviroment)]
        [InlineData(MethodDirection.BunkerObject)]
        [InlineData(MethodDirection.BunkerWall)]
        [InlineData(MethodDirection.ItemBunker)]
        public async void UseChangeBunkerComponentCard_ShouldInvoke_ChangeBunkerComponentCommand(MethodDirection methodDirection)
        {
            var character = GetCharacterWithConcreteCard(methodDirection, MethodType.Change);
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(x => x.GetCharacterById(character.Id, true)).ReturnsAsync(character);
            var mediator = new Mock<IMediator>();
            IRequestHandler<UseCardNoneTargetCommand, Unit> cardUseCommand =
                new UseCardNoneTargetCommandHandler( mediator.Object, characterRepository.Object);

            var result = await cardUseCommand.Handle(new UseCardNoneTargetCommand(character.Id,1), default);
            Assert.Equal(Unit.Value, result);

            mediator.Verify(x => x.Send(It.Is<object>(c => c.GetType() == typeof(ChangeBunkerComponentCommand)), default), Times.Once);
        }
        [Theory]
        [InlineData(MethodDirection.Trait)]
        [InlineData(MethodDirection.AdditionalInformation)]
        [InlineData(MethodDirection.Profession)]
        [InlineData(MethodDirection.CharacterItem)]
        [InlineData(MethodDirection.Phobia)]
        [InlineData(MethodDirection.Age)]
        [InlineData(MethodDirection.Hobby)]
        [InlineData(MethodDirection.Health)]
        public async void UseSpyCharacteristicCard_ShouldInvoke_SpyCharacterCharacteristicCommand(MethodDirection methodDirection)
        {
            var character = GetCharacterWithConcreteCard(methodDirection, MethodType.Spy);
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(x => x.GetCharacterById(character.Id, true)).ReturnsAsync(character);
            var mediator = new Mock<IMediator>();
            IRequestHandler<UseCardOnOtherCharacterCommand, Unit> cardUseCommand =
                new UseCardOnOtherCharacterCommandHandler( characterRepository.Object, mediator.Object);

            var result = await cardUseCommand.Handle(new UseCardOnOtherCharacterCommand(1, character.Id,character.Id), default);
            Assert.Equal(Unit.Value, result);

            mediator.Verify(x => x.Send(It.Is<object>(c => c.GetType() == typeof(SpyCharacterComponentCommand)), default), Times.Once);
        }
        public static Character GetCharacterWithConcreteCard(MethodDirection methodDirection,MethodType methodType)
        {
            var cardFirst = new Card("test", new CardMethod(methodType, methodDirection));
            var cardSecond = new Card("test1", new CardMethod(methodType, methodDirection));
            var character = CharacterCreator.CreateCharacter();
            character.UpdateCards(new List<Card> { cardFirst, cardSecond });
            character.RegisterCharacterInGame(1, 0);
            return character;
        }
    }
}
