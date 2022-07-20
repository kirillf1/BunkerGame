using BunkerGame.Domain.Characters.Cards;
using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponents.Domain.CharacterComponents.Cards;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGameComponents.Domain.BunkerComponents;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGameComponents.Domain.Catastrophes;
using BunkerGame.Domain.GameSessions;
using BunkerGameComponents.Domain.ExternalSurroundings;

namespace BunkerGame.VkApi.VkGame.Characters
{
    public class CardFactory : ICardFactory
    {
        private readonly IGameComponentsRepository gameComponentsRepository;
        private Random random;
        public CardFactory(IGameComponentsRepository gameComponentsRepository)
        {
            this.gameComponentsRepository = gameComponentsRepository;
            random = new Random();
        }
        public async Task<Card> Create()
        {
            var cardComponent = await gameComponentsRepository.GetComponent<CharacterCard>(true);
            return await ConvertComponentToCard(cardComponent);
        }

        public async Task<IEnumerable<Card>> CreateCards(int count)
        {
            var cardComponents = await gameComponentsRepository.GetComponents<CharacterCard>(0, count, true);
            var newCards = new List<Card>(count);
            foreach (var cardComponent in cardComponents)
            {
                newCards.Add(await ConvertComponentToCard(cardComponent));
            }
            return newCards;
        }
        private async Task<Card> ConvertComponentToCard(CharacterCard characterCard)
        {
            var cardMethod = characterCard.CardMethod;
            object? component = null;
            if (cardMethod.ItemId != null)
                component = await GetComponent(cardMethod.ItemId, cardMethod.MethodDirection);
            return new Card(characterCard.Description, new CardMethod(cardMethod.MethodType, cardMethod.MethodDirection, component), false);
        }
        private async Task<object?> GetComponent(ComponentId componentId, MethodDirection methodDirection)
        {
            switch (methodDirection)
            {
                case MethodDirection.AdditionalInformation:
                    return await ConvertComponent<CharacterAdditionalInformation, AdditionalInformation>(componentId,
                        c => new AdditionalInformation(c.Description, c.Value, c.AddInfType));
                case MethodDirection.Health:
                    return await ConvertComponent<CharacterHealth, Health>(componentId,
                       c => new Health(c.Description, c.Value, c.HealthType));
                case MethodDirection.Profession:
                    return await ConvertComponent<CharacterProfession, Profession>(componentId,
                      c => new Profession(c.Description, c.Value, c.ProfessionSkill, c.ProfessionType, (byte)random.Next(0, 10)));
                case MethodDirection.Phobia:
                    return await ConvertComponent<CharacterPhobia, Phobia>(componentId,
                        c => new Phobia(c.Description, c.Value, c.PhobiaDebuffType));
                case MethodDirection.Sex:
                    return new Sex(componentId.Id == 1);
                case MethodDirection.Trait:
                    return await ConvertComponent<CharacterTrait, Trait>(componentId,
                        c => new Trait(c.Description, c.Value, c.TraitType));
                case MethodDirection.Hobby:
                    return await ConvertComponent<CharacterHobby, Hobby>(componentId,
                        c => new Hobby(c.Description, c.Value, c.HobbyType, (byte)random.Next(0, 5)));
                case MethodDirection.Age:
                    return new Age(componentId.Id);
                case MethodDirection.CharacterItem:
                    return await ConvertComponent<CharacterItem, Domain.Characters.CharacterComponents.Item>(componentId,
                        c => new Domain.Characters.CharacterComponents.Item(c.Description, c.Value, c.CharacterItemType, false));
                case MethodDirection.Childbearing:
                    return new Childbearing(componentId.Id == 1);
                case MethodDirection.BunkerWall:
                    return await ConvertComponent<BunkerWall, Condition>(componentId,
                        c => new Condition(c.Value, c.Description, c.BunkerState));
                case MethodDirection.ItemBunker:
                    return await ConvertComponent<ItemBunker, Domain.GameSessions.Bunkers.Item>(componentId,
                         c => new Domain.GameSessions.Bunkers.Item(c.Value, c.Description, c.ItemBunkerType));
                case MethodDirection.BunkerObject:
                    return await ConvertComponent<BunkerObject, Building>(componentId,
                        c => new Building(c.Value, c.Description, c.BunkerObjectType));
                case MethodDirection.BunkerEnviroment:
                    return await ConvertComponent<BunkerEnviroment, Enviroment>(componentId,
                        c => new Enviroment(c.Description, c.Value, c.EnviromentBehavior, c.EnviromentType));
                case MethodDirection.Catastrophe:
                    return await ConvertComponent<GameCatastrophe, Catastrophe>(componentId,
                        c => new Catastrophe(c.CatastropheType, c.DestructionPercent, c.SurvivedPopulationPercent, c.Description, c.Value,
                        c.HidingTerm));
                case MethodDirection.ExternalSurrounding:
                    return await ConvertComponent<GameExternalSurrounding, ExternalSurrounding>(componentId,
                        c => new ExternalSurrounding(c.Description, c.Value, c.SurroundingType));
                default:
                    return null;
            }

        }
        private async Task<V> ConvertComponent<T, V>(ComponentId componentId, Func<T, V> convert) where T : class, IGameComponent
        {
            var component = await gameComponentsRepository.GetComponent<T>(componentId);
            return convert(component);
        }
    }
}
