using BunkerGame.Application.Bunkers.ChangeBunkerComponent;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer
{
    public static class ChangeBunkerComponentCommandFactory
    {
        public static object CreateChangeBunkerComponentCommand(long gameSessionId,int? componentId,MethodDirection methodDirection)
        {
            return methodDirection switch
            {
                MethodDirection.BunkerWall => GetChangeBunkerComponentCommand<BunkerWall>(gameSessionId, componentId),
                MethodDirection.BunkerSize => GetChangeBunkerComponentCommand<BunkerSize>(gameSessionId, componentId),
                MethodDirection.Supplies => GetChangeBunkerComponentCommand<Supplies>(gameSessionId, componentId),
                MethodDirection.ItemBunker => GetChangeBunkerComponentCollectionCommand<ItemBunker>(gameSessionId, componentId),
                MethodDirection.BunkerObject => GetChangeBunkerComponentCollectionCommand<BunkerObject>(gameSessionId, componentId),
                MethodDirection.BunkerEnviroment => GetChangeBunkerComponentCommand<BunkerEnviroment>(gameSessionId, componentId),
                _ => throw new NotImplementedException("No such command for bunker component with method direction " + methodDirection),
            };
        }
        private static ChangeBunkerComponentCollectionCommand<T> GetChangeBunkerComponentCollectionCommand<T>(long gameSessionId, int? componentId) 
            where T: BunkerComponent
        {
            return new ChangeBunkerComponentCollectionCommand<T>(gameSessionId, componentId);
        }
        private static ChangeBunkerComponentCommand<T> GetChangeBunkerComponentCommand<T>(long gameSessionId, int? componentId) where T : BunkerComponent
        {
            return new ChangeBunkerComponentCommand<T>(gameSessionId, componentId);
        }
    }

}
