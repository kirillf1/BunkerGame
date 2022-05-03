using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;

namespace BunkerGame.Application.Characters.CharacterFactories
{
    public class CharacterFactoryBase : ICharacterFactory
    {
        private readonly ICharacterComponentRepLocator characterComponentRep;
        protected const byte Cards_Count = 2;
        protected Random random;
        public CharacterFactoryBase(ICharacterComponentRepLocator characterComponentRep)
        {
            this.characterComponentRep = characterComponentRep;
            random = new Random();
        }
        public Task<Character> CreateCharacter(CharacterOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Character>> CreateCharacters(int count, CharacterOptions options)
        {
            var cards = new Stack<Card>(await characterComponentRep.GetCharacterComponents<Card>(count * Cards_Count, true, c => !c.IsSpecial));
            //cards.Push(await characterComponentRep.GetCharacterComponent<Card>(true, c => c.CardMethod.MethodDirection == Domain.Characters.CharacterComponents.Cards.MethodDirection.FreePlace));
            //cards.Push(await characterComponentRep.GetCharacterComponent<Card>(true, c => c.Id == 60));
            var addInfs = new Stack<AdditionalInformation>(await characterComponentRep.GetCharacterComponents<AdditionalInformation>(count));
            var professions = new Stack<Profession>(await characterComponentRep.GetCharacterComponents<Profession>(count));
            var traits = new Stack<Trait>(await characterComponentRep.GetCharacterComponents<Trait>(count));
            var characterItems = new Stack<CharacterItem>(await characterComponentRep.GetCharacterComponents<CharacterItem>(count));
            var healths = new Stack<Health>(await GetHealths(count, options.FullHealthChance));
            var sizes = new Stack<Size>(await characterComponentRep.GetCharacterComponents<Size>(count));
            var ages = new Stack<Age>(await characterComponentRep.GetCharacterComponents<Age>(count));
            var phobias = new Stack<Phobia>(await GetPhobias(count, options.NotPhobiaChance));
            var childbering = new Stack<Childbearing>(await characterComponentRep.GetCharacterComponents<Childbearing>(count));
            var sexs = new Stack<Sex>(await characterComponentRep.GetCharacterComponents<Sex>(count));
            var hobbies = new Stack<Hobby>(await characterComponentRep.GetCharacterComponents<Hobby>(count));

            var characters = new List<Character>(count);
            for (int i = 0; i < count; i++)
            {
                var character = new Character(professions.Pop(), sexs.Pop(), ages.Pop(), childbering.Pop(),
                    sizes.Pop(), healths.Pop(), traits.Pop(), phobias.Pop(), hobbies.Pop(), addInfs.Pop(), characterItems.Pop(),
                    new List<Card>(2) { cards.Pop(), cards.Pop() });
                character.ChangeLive(options.IsAlive);
                character.SetCharacterExpirience((byte)random.Next(0, 5), (byte)random.Next(0, 10));
                characters.Add(character);
            }
            return characters;
        }
        private async Task<IEnumerable<Health>> GetHealths(int count, double fullHealthChance)
        {
            List<Health> healths = new List<Health>(count);
            int fullHealthCounter = 0;
            int diseaseCounter = 0;
            for (int i = 0; i <= count; i++)
            {
                if (random.Next(0, 100) <= fullHealthChance)
                    fullHealthCounter++;
                else
                    diseaseCounter++;
            }
            if (fullHealthCounter > 0)
            {
                var fullHealth = await characterComponentRep.GetCharacterComponent<Health>(predicate: c => c.HealthType == HealthType.FullHealth);
                for (int i = 0; i <= fullHealthCounter; i++)
                {
                    healths.Add(fullHealth);
                }
            }
            healths.AddRange(await characterComponentRep.GetCharacterComponents<Health>(diseaseCounter, true, c => c.HealthType != HealthType.FullHealth));
            return healths.OrderBy(_ => Guid.NewGuid());
        }
        private async Task<IEnumerable<Phobia>> GetPhobias(int count, double noPhobiaChance)
        {
            List<Phobia> phobias = new(count);
            int noPhobiaCounter = 0;
            int withPhobiaCounter = 0;
            for (int i = 0; i <= count; i++)
            {
                if (random.Next(0, 100) <= noPhobiaChance)
                    noPhobiaCounter++;
                else
                    withPhobiaCounter++;
            }
            if (noPhobiaCounter > 0)
            {
                var noPhobia = await characterComponentRep.GetCharacterComponent<Phobia>(predicate: c => c.PhobiaDebuffType == PhobiaDebuffType.None);
                for (int i = 0; i <= noPhobiaCounter; i++)
                {
                    phobias.Add(noPhobia);
                }
            }
            phobias.AddRange(await characterComponentRep.GetCharacterComponents<Phobia>(withPhobiaCounter, true, c => c.PhobiaDebuffType != PhobiaDebuffType.None));
            return phobias.OrderBy(_ => Guid.NewGuid());
        }
    }
}

