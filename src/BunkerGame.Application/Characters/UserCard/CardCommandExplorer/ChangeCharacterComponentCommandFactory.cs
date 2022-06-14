using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer
{
    public static class ChangeCharacterComponentCommandFactory
    {
        private static Dictionary<MethodDirection, Func<int, int?, object>> ChangeCharacterComponentCommands;
        static ChangeCharacterComponentCommandFactory()
        {
            ChangeCharacterComponentCommands = new();
            InitChangeComponentCommands();
        }
        public static object CreateChangeCharacterComponentCommand(int characterId, int? componentId, MethodDirection methodDirection)
        {
            if (ChangeCharacterComponentCommands.TryGetValue(methodDirection, out var command))
                return command.Invoke(characterId, componentId);
            throw new NotImplementedException($"Character don't contains this property - {methodDirection}");

        }
        private static ChangeCharacteristicCommand<T> GetChangeCharacteristicCommand<T>(int characterId, int? componentId)
          where T : CharacterEntity
        {
            var changeMethod = CreateUpdateCharacterMethod<T>();
            return new ChangeCharacteristicCommand<T>(characterId, componentId, changeMethod);
        }
        private static Func<Character, int?, ICharacterComponentRepository<T>, Task<T>> CreateUpdateCharacterMethod<T>()
            where T : CharacterEntity
        {
            return ChangeCharacterComponent<T>;
        }
        private static async Task<T> ChangeCharacterComponent<T>(Character character, int? componentId,
            ICharacterComponentRepository<T> characterComponentRepository) where T : CharacterEntity
        {
            var characterProxy = new CharacterProxy(character);
            if (characterProxy.IsContainsCharacterComponent<T>())
                return await Change(characterProxy.GetCharacterComponent<T>(), componentId, characterComponentRepository);
            else if (characterProxy.IsContainsCharacterComponentCollection<T>())
                return await ChangeCollection(characterProxy.GetCharacterComponentCollection<T>(), componentId, characterComponentRepository);
            throw new NotImplementedException();
        }
        private static async Task<T> Change<T>(ICharacterComponent<T> characterComponent, int? componentId,
            ICharacterComponentRepository<T> characterComponentRepository) where T : CharacterEntity
        {
            characterComponent.Component = await characterComponentRepository.GetCharacterComponent(true,
                p => componentId.HasValue ? p.Id == componentId.Value : p.Id != characterComponent.Component.Id);
            return characterComponent.Component;
        }
        private static async Task<T> ChangeCollection<T>(ICharacterComponentCollection<T> characterComponents, int? componentId,
            ICharacterComponentRepository<T> characterComponentRepository)
             where T : CharacterEntity
        {
            var newComponents = new List<T>();
            var componentsCount = characterComponents.Components.Count;
            if (componentId.HasValue)
            {
                newComponents.Add(await characterComponentRepository.GetCharacterComponent(true, c => c.Id == componentId.Value));
                componentsCount--;
            }
            switch (componentsCount)
            {
                case 1:
                    newComponents.Add(await characterComponentRepository.GetCharacterComponent(true,
                        c => characterComponents.Components.Any(i => i.Id != c.Id)));
                    break;
                case int count when count > 1:
                    newComponents.AddRange(await characterComponentRepository.GetCharacterComponents(count, true,
                         c => characterComponents.Components.Any(i => i.Id != c.Id)));
                    break;
            }
            characterComponents.Components = newComponents;
            return characterComponents.Components.First();
        }
        private static ChangeCharacteristicCommand<Sex> GetChangeSexCommand(int characterId, int? sex)
        {
            var command = new Func<Character, int?, ICharacterComponentRepository<Sex>, Task<Sex>>(
                async (character, component, repository) =>
                {
                    // if component == 1 return man
                    var sex = component.HasValue ? new Sex(component == 1) : await repository.GetCharacterComponent();
                    character.UpdateSex(sex);
                    return sex;
                });
            return new ChangeCharacteristicCommand<Sex>(characterId, sex, command);
        }
        private static ChangeCharacteristicCommand<Childbearing> GetChangeChildbearingCommand(int characterId, int? childBearing)
        {
            var command = new Func<Character, int?, ICharacterComponentRepository<Childbearing>, Task<Childbearing>>(
                async (character, component, repository) =>
                {
                    // if component == 1 canGiveBirth
                    var childbearing = component.HasValue ? new Childbearing(component == 1) : await repository.GetCharacterComponent();
                    character.UpdateChildbearing(childbearing);
                    return childbearing;
                });
            return new ChangeCharacteristicCommand<Childbearing>(characterId, childBearing, command);
        }
        private static ChangeCharacteristicCommand<Age> GetChangeAgeCommand(int characterId, int? years)
        {
            var command = new Func<Character, int?, ICharacterComponentRepository<Age>, Task<Age>>(
                async (character, years, repository) =>
                {
                    var age = years.HasValue ? new Age(years.Value) : await repository.GetCharacterComponent();
                    character.UpdateAge(age);
                    return age;
                });
            return new ChangeCharacteristicCommand<Age>(characterId, years, command);
        }
        private static ChangeCharacteristicCommand<Size> GetChangeSizeCommand(int characterId)
        {
            var command = new Func<Character, int?, ICharacterComponentRepository<Size>, Task<Size>>(
                async (character, nuint, repository) =>
                {
                    var size = await repository.GetCharacterComponent();
                    character.UpdateSize(size);
                    return size;
                });
            return new ChangeCharacteristicCommand<Size>(characterId, null, command);
        }
        private static void InitChangeComponentCommands()
        {
            ChangeCharacterComponentCommands[MethodDirection.Trait] =
                (characterId, componentId) => GetChangeCharacteristicCommand<Trait>(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.AdditionalInformation] =
                (characterId, componentId) => GetChangeCharacteristicCommand<AdditionalInformation>(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.Health] =
                (characterId, componentId) => GetChangeCharacteristicCommand<Health>(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.Hobby] =
                (characterId, componentId) => GetChangeCharacteristicCommand<Hobby>(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.CharacterItem] =
                (characterId, componentId) => GetChangeCharacteristicCommand<CharacterItem>(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.Profession] =
                (characterId, componentId) => GetChangeCharacteristicCommand<Profession>(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.Phobia] =
                (characterId, componentId) => GetChangeCharacteristicCommand<Phobia>(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.Sex] =
                (characterId, componentId) => GetChangeSexCommand(characterId, componentId);
            ChangeCharacterComponentCommands[MethodDirection.Age] =
                (characterId, years) => GetChangeAgeCommand(characterId, years);
            ChangeCharacterComponentCommands[MethodDirection.Childbearing] =
               (characterId, years) => GetChangeChildbearingCommand(characterId, years);
            ChangeCharacterComponentCommands[MethodDirection.Size] =
                 (characterId, componentId) => GetChangeSizeCommand(characterId);
        }
    }
}
