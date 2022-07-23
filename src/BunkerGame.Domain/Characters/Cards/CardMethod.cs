using BunkerGame.Domain.Characters.Cards.CardCommandExplorer;
using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.Cards
{
    public record CardMethod : Value<CardMethod>
    {
        public CardMethod(MethodType methodType, MethodDirection methodDirection, object? item)
        {
            MethodType = methodType;
            MethodDirection = methodDirection;
            Item = item;
        }
        public MethodType MethodType { get; }
        public MethodDirection MethodDirection { get; }
        public object? Item { get; }
        public void SetGetCommand(CardParams cardParams, CardCommandResultBuilder resultBuilder)
        {
            if (IsTargetCharacterCard())
            {
                var targetCharacterId = cardParams.TargetCharacter;
                if (targetCharacterId?.Value == null)
                {
                    resultBuilder.AddError(CardExecuteError.NoTargetCharacter);
                    return;
                }
                var targetCardParams = new TargetCharacterCardArgs(cardParams.CardUserId, targetCharacterId, cardParams.GameSessionId, this);
                CommandExplorerByCard.SetTargetCharacterCommand(targetCardParams, resultBuilder);
                return;
            }
            var noTargetParams = new NoneTargetCardArgs(cardParams.CardUserId, cardParams.GameSessionId, this);
            CommandExplorerByCard.SetNoTargetCommand(noTargetParams, resultBuilder);

        }
        public bool IsTargetCharacterCard()
        {
            var methodGroup = DefineDirectionGroup();
            if (methodGroup == DirectionGroup.Character)
            {
                if (MethodType == MethodType.Update || MethodType == MethodType.SpyYourself)
                    return false;
                return true;
            }
            return false;
        }
        public DirectionGroup DefineDirectionGroup()
        {
            return MethodDirection switch
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
                MethodDirection.Card => DirectionGroup.Character,
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
        public enum DirectionGroup
        {
            Unknown,
            Character,
            Bunker,
            Catastrophe,
            GameSession
        }
    }
}
