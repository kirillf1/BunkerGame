using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer
{
    internal static class SpyCharacterComponentCommandFactory
    {
        private static Dictionary<MethodDirection, Func<int, object>> spyCommands;
        
        static SpyCharacterComponentCommandFactory()
        {
            spyCommands = new();
            InitSpyCommands();
        }
       
        public static object CreateSpyCharacterComponentCommand(int characterId,MethodDirection methodDirection)
        {
            if (!spyCommands.TryGetValue(methodDirection, out var command))
                throw new NotImplementedException($"Can't create command for {methodDirection}");
            return command.Invoke(characterId);
        }
        private static SpyCharacterComponentCommand<T> GetSpyComponentCommand<T>(int characterId) where T : CharacterComponent
        {
            return new SpyCharacterComponentCommand<T>(characterId);
        }
        
        private static void InitSpyCommands()
        {
            spyCommands[MethodDirection.Trait] = (characterId) => GetSpyComponentCommand<Trait>(characterId);
            spyCommands[MethodDirection.Sex] = (id) => GetSpyComponentCommand<Sex>(id);
            spyCommands[MethodDirection.CharacterItem] = (id) => GetSpyComponentCommand<CharacterItem>(id);
            spyCommands[MethodDirection.Childbearing] = (id) => GetSpyComponentCommand<Childbearing>(id);
            spyCommands[MethodDirection.AdditionalInformation] = (id) => GetSpyComponentCommand<AdditionalInformation>(id);
            spyCommands[MethodDirection.Phobia] = (id) => GetSpyComponentCommand<Phobia>(id);
            spyCommands[MethodDirection.Age] = (id) => GetSpyComponentCommand<Age>(id);
            spyCommands[MethodDirection.Size] = (id) => GetSpyComponentCommand<Size>(id);
            spyCommands[MethodDirection.Profession] = (id) => GetSpyComponentCommand<Profession>(id);
            spyCommands[MethodDirection.Health] = (id) => GetSpyComponentCommand<Health>(id);
            spyCommands[MethodDirection.Hobby] = (id) => GetSpyComponentCommand<Hobby>(id);
        }
    }
}
