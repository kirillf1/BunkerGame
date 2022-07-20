using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.Cards;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Shared;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.CharacterComponents;
using System.Linq.Expressions;
using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.VkApi.VkGame.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        const byte NoPhobiaChance = 30;
        const byte FullHealthChance = 30;

        private readonly IGameComponentsRepository gameComponentsRepository;
        private readonly ICardFactory cardFactory;
        private readonly CharacterComponentGenerator characterComponentGenerator;
        private readonly Random random;

        public CharacterFactory(IGameComponentsRepository gameComponentsRepository, ICardFactory cardFactory, CharacterComponentGenerator characterComponentGenerator)
        {
            this.gameComponentsRepository = gameComponentsRepository;
            this.cardFactory = cardFactory;
            this.characterComponentGenerator = characterComponentGenerator;
            random = new Random();
        }
        public async Task<Character> CreateCharacter(CharacterId characterId, PlayerId playerId, GameSessionId gameSessionId)
        {
            var phobia = await GetRandomPhobia();
            var health = await GetRandomHealth();
            var profession = await GetRandomProfession();
            var hobby = await GetRandomHobby();
            var trait = await ConvertComponent<CharacterTrait, Trait>(c => new Trait(c.Description, c.Value, c.TraitType));
            var addInf = await ConvertComponent<CharacterAdditionalInformation, AdditionalInformation>(
                        c => new AdditionalInformation(c.Description, c.Value, c.AddInfType));
            var sex = characterComponentGenerator.GenerateSex();
            var size = characterComponentGenerator.GenerateSize();
            var age = characterComponentGenerator.GenerateAge(profession.Experience);
            var childbearing = characterComponentGenerator.GenerateChildbearing();
            var item = await ConvertComponent<CharacterItem, Item>(c => new Item(c.Description, c.Value, c.CharacterItemType, false));
            var cards = await cardFactory.CreateCards(2);

            return new Character(characterId, playerId, gameSessionId, cards, new List<Item> { item }, phobia, health, sex,
                addInf, childbearing, profession, age, size, trait, hobby);
        }
        public Task<IEnumerable<Character>> CreateCharacters(IEnumerable<(CharacterId, PlayerId)> characterIds, GameSessionId gameSessionId)
        {
            throw new NotImplementedException();
        }
        private async Task<Profession> GetRandomProfession()
        {
            // todo добавить вытягивание карт
            return await ConvertComponent<CharacterProfession, Profession>(
                      c => new Profession(c.Description, c.Value, c.ProfessionSkill, c.ProfessionType, (byte)random.Next(0, 10)));
        }
        private async Task<Hobby> GetRandomHobby()
        {
            return await ConvertComponent<CharacterHobby, Hobby>(
                        c => new Hobby(c.Description, c.Value, c.HobbyType, (byte)random.Next(0, 5)));
        }
        private async Task<Phobia> GetRandomPhobia()
        {
            var isNoPhobia = random.Next(0, 100) < NoPhobiaChance;
            return await ConvertComponent<CharacterPhobia, Phobia>(c => new Phobia(c.Description, c.Value, c.PhobiaDebuffType),
                isNoPhobia ? c => c.PhobiaDebuffType == PhobiaDebuffType.None : null);
        }
        private async Task<Health> GetRandomHealth()
        {
            var isFullHealth = random.Next(0, 100) < FullHealthChance;
            return await ConvertComponent<CharacterHealth, Health>(c => new Health(c.Description, c.Value, c.HealthType),
                isFullHealth ? c => c.HealthType == HealthType.FullHealth : null);
        }
        private async Task<V> ConvertComponent<T, V>(Func<T, V> convert, Expression<Func<T, bool>>? expression = null) where T : class, IGameComponent
        {
            var component = await gameComponentsRepository.GetComponent(true, expression);
            return convert(component);
        }
        private async Task<IEnumerable<V>> ConvertComponents<T, V>(int count, Func<T, V> convert, Expression<Func<T, bool>>? expression = null) where T : class, IGameComponent
        {
            var components = await gameComponentsRepository.GetComponents(0, count, true, expression);
            var values = new List<V>(count);
            foreach (var component in components)
            {
                values.Add(convert(component));
            }
            return values;
        }
    }
}
