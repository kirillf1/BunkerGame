using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Framework;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.CharacterComponents;
using MediatR;
using System.Linq.Expressions;

namespace BunkerGame.VkApi.VkGame.Characters.CommandHandlers
{
    public class UpdateCharacterFromRepositoryHandler : IRequestHandler<Commands.UpdateAdditionalInformation>,
        IRequestHandler<Commands.UpdateHealth>, IRequestHandler<Commands.UpdateHobby>,
        IRequestHandler<Commands.UpdateItem>, IRequestHandler<Commands.UpdatePhobia>,
        IRequestHandler<Commands.UpdateProfession>, IRequestHandler<Commands.UpdateTrait>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IEventStore eventStore;
        private readonly IGameComponentsRepository gameComponentsRepository;
        private readonly Random random;
        public UpdateCharacterFromRepositoryHandler(ICharacterRepository characterRepository, IEventStore eventStore, IGameComponentsRepository gameComponentsRepository)
        {
            this.characterRepository = characterRepository;
            this.eventStore = eventStore;
            this.gameComponentsRepository = gameComponentsRepository;
            random = new Random();
        }

        public async Task<Unit> Handle(Commands.UpdateAdditionalInformation request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var additionalInformation = request.AdditionalInformation;
            if (additionalInformation == null)
            {
                var component = await GetRandomComponent<CharacterAdditionalInformation>();
                additionalInformation = new AdditionalInformation(component.Description, component.Value, component.AddInfType);
            }
            character.UpdateAdditionalInf(additionalInformation);
            return await SaveChanges(character);
        }

        public async Task<Unit> Handle(Commands.UpdateHealth request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var health = request.Health;
            if (health == null)
            {
                int fullHealthChance = 30;
                bool isFullHealth = random.Next(0, 100) < fullHealthChance;
                var component = await GetRandomComponent<CharacterHealth>(isFullHealth ?
                    h => h.HealthType == GameTypes.CharacterTypes.HealthType.FullHealth : null);
                health = new Health(component.Description, component.Value, component.HealthType);
            }
            character.UpdateHealth(health);
            return await SaveChanges(character);
        }

        public async Task<Unit> Handle(Commands.UpdateHobby request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var hobby = request.Hobby;
            if (hobby == null)
            {
                var component = await GetRandomComponent<CharacterHobby>();
                hobby = new Hobby(component.Description, component.Value, component.HobbyType, (byte)random.Next(0, 5));
            }
            character.UpdateHobby(hobby);
            return await SaveChanges(character);
        }

        public async Task<Unit> Handle(Commands.UpdateItem request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var item = request.Item;
            if (item == null)
            {
                var component = await GetRandomComponent<CharacterItem>();
                item = new Item(component.Description, component.Value, component.CharacterItemType, false);
            }
            character.UpdateItem(item);
            return await SaveChanges(character);
        }

        public async Task<Unit> Handle(Commands.UpdatePhobia request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var phobia = request.Phobia;
            if (phobia == null)
            {
                int noPhobiaChance = 30;
                bool isNoPhobia = random.Next(0, 100) < noPhobiaChance;
                var component = await GetRandomComponent<CharacterPhobia>(isNoPhobia ?
                    p => p.PhobiaDebuffType == GameTypes.CharacterTypes.PhobiaDebuffType.None : null);
                phobia = new Phobia(component.Description, component.Value, component.PhobiaDebuffType);
            }
            character.UpdatePhobia(phobia);
            return await SaveChanges(character);
        }

        public async Task<Unit> Handle(Commands.UpdateProfession request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var profession = request.Profession;
            if (profession == null)
            {
                var component = await GetRandomComponent<CharacterProfession>(c => c.ProfessionType != character.Profession.ProfessionType);
                profession = new Profession(component.Description, component.Value, component.ProfessionSkill, component.ProfessionType,
                    (byte)random.Next(0, 10));
                //todo добавить извлечения из компонента карт и предметов
            }
            character.UpdateProfession(profession);
            return await SaveChanges(character);
        }

        public async Task<Unit> Handle(Commands.UpdateTrait request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var trait = request.Trait;
            if (trait == null)
            {
                var component = await GetRandomComponent<CharacterTrait>();
                trait = new Trait(component.Description, component.Value, component.TraitType);
            }
            character.UpdateTrait(trait);
            return await SaveChanges(character);
        }
        private async Task<T> GetRandomComponent<T>(Expression<Func<T, bool>>? expression = null) where T : class, IGameComponent
        {
            return await gameComponentsRepository.GetComponent(true, expression);
        }

        private async Task<Unit> SaveChanges(Character character)
        {
            await eventStore.Save(character);
            return Unit.Value;
        }
    }
}
