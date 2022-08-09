using BunkerGameComponents.Domain.BunkerComponents;
using BunkerGameComponents.Domain.Catastrophes;
using BunkerGameComponents.Domain.CharacterComponents.Cards;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponents.Domain.ExternalSurroundings;
using BunkerGameComponetns.Editor.Model;
using BunkerGameComponetns.Editor.View;

namespace BunkerGameComponetns.Editor.Services
{
    public record GameComponentsQuery(GameComponentType GameComponentType, int SkipCount, int Count, string DescriptionQuery = "");
    public class GameComponentsService
    {
        private readonly IGameComponentsRepository gameComponentsRepository;
        private readonly IUnitOfWork unitOfWork;

        public GameComponentsService(IGameComponentsRepository gameComponentsRepository, IUnitOfWork unitOfWork)
        {
            this.gameComponentsRepository = gameComponentsRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IGameComponent> AddEmptyComponent(GameComponentType gameComponentType)
        {
            var componentId = await GetComponentIdAsync(gameComponentType);
            switch (gameComponentType)
            {
                case GameComponentType.Catastrophe:
                    var catastrophe = new GameCatastrophe(componentId);
                    await AddComponentToRepository(catastrophe);
                    return catastrophe;
                case GameComponentType.BunkerEnviroment:
                    var enviroment = new BunkerEnviroment(componentId);
                    await AddComponentToRepository(enviroment);
                    return enviroment;
                case GameComponentType.BunkerWall:
                    var wall = new BunkerWall(componentId);
                    await AddComponentToRepository(wall);
                    return wall;
                case GameComponentType.ItemBunker:
                    var bunkerItem = new ItemBunker(componentId);
                    await AddComponentToRepository(bunkerItem);
                    return bunkerItem;
                case GameComponentType.BunkerObject:
                    var bunkerobj = new BunkerObject(componentId);
                    await AddComponentToRepository(bunkerobj);
                    return bunkerobj;
                case GameComponentType.Phobia:
                    var phobia = new CharacterPhobia(componentId);
                    await AddComponentToRepository(phobia);
                    return phobia;
                case GameComponentType.Hobby:
                    var hobby = new CharacterHobby(componentId);
                    await AddComponentToRepository(hobby);
                    return hobby;
                case GameComponentType.AdditionalInformation:
                    var addInf = new CharacterAdditionalInformation(componentId);
                    await AddComponentToRepository(addInf);
                    return addInf;
                case GameComponentType.Health:
                    var health = new CharacterHealth(componentId);
                    await AddComponentToRepository(health);
                    return health;
                case GameComponentType.CharacterItem:
                    var item = new CharacterItem(componentId);
                    await AddComponentToRepository(item);
                    return item;
                case GameComponentType.Profession:
                    var profession = new CharacterProfession(componentId);
                    await AddComponentToRepository(profession);
                    return profession;
                case GameComponentType.Trait:
                    var trait = new CharacterTrait(componentId);
                    await AddComponentToRepository(trait);
                    return trait;
                case GameComponentType.Card:
                    var card = new CharacterCard(componentId);
                    await AddComponentToRepository(card);
                    return card;
                case GameComponentType.ExternalSurrounding:
                    var externalSurrounding = new GameExternalSurrounding(componentId);
                    await AddComponentToRepository(externalSurrounding);
                    return externalSurrounding;
                default:
                    throw new NotImplementedException(nameof(GameComponentType));
            }

        }
        public async Task NavigateToDetails(IGameComponent gameComponent)
        {
            if (gameComponent is GameCatastrophe)
                await GoToDetails(nameof(CatastropheDetails), gameComponent);
            else if (gameComponent is BunkerWall)
                await GoToDetails(nameof(BunkerWallDetails), gameComponent);
            else if (gameComponent is CharacterItem)
                await GoToDetails(nameof(CharacterItemDetails), gameComponent);
            else if (gameComponent is CharacterAdditionalInformation)
                await GoToDetails(nameof(AdditionalInformationDetails), gameComponent);
            else if (gameComponent is CharacterProfession)
                await GoToDetails(nameof(ProfessionDetails), gameComponent);
            else if (gameComponent is BunkerEnviroment)
                await GoToDetails(nameof(BunkerEnviromentDetails), gameComponent);
            else if (gameComponent is BunkerObject)
                await GoToDetails(nameof(BunkerObjectDetails), gameComponent);
            else if (gameComponent is ItemBunker)
                await GoToDetails(nameof(BunkerItemDetails), gameComponent);
            else if (gameComponent is CharacterPhobia)
                await GoToDetails(nameof(PhobiaDetails), gameComponent);
            else if (gameComponent is CharacterCard)
                await GoToDetails(nameof(CardDetails), gameComponent);
            else if (gameComponent is CharacterHobby)
                await GoToDetails(nameof(HobbyDetails), gameComponent);
            else if (gameComponent is CharacterHealth)
                await GoToDetails(nameof(HealthDetails), gameComponent);
            else if (gameComponent is CharacterTrait)
                await GoToDetails(nameof(TraitDetails), gameComponent);
            else if (gameComponent is GameExternalSurrounding)
                await GoToDetails(nameof(ExternalSurroundingDetails), gameComponent);
            else
               await Shell.Current.DisplayAlert("Not found", "No such page", "OK");
        }
        private static async Task GoToDetails(string pageName, object component)
        {
            await Shell.Current.GoToAsync(pageName, true,
            new Dictionary<string, object>
            {
                ["GameComponent"] = component
            }) ;
        } 
        public async Task<bool> TryDeleteComponentByGameComponentType(GameComponentType gameComponentType, IGameComponent gameComponent)
        {
            return gameComponentType switch
            {
                GameComponentType.Catastrophe => await TryDeleteComponent<GameCatastrophe>(gameComponent),
                GameComponentType.BunkerEnviroment => await TryDeleteComponent<BunkerEnviroment>(gameComponent),
                GameComponentType.BunkerWall => await TryDeleteComponent<BunkerWall>(gameComponent),
                GameComponentType.ItemBunker => await TryDeleteComponent<ItemBunker>(gameComponent),
                GameComponentType.BunkerObject => await TryDeleteComponent<BunkerObject>(gameComponent),
                GameComponentType.Phobia => await TryDeleteComponent<CharacterPhobia>(gameComponent),
                GameComponentType.Hobby => await TryDeleteComponent<CharacterHobby>(gameComponent),
                GameComponentType.AdditionalInformation => await TryDeleteComponent<CharacterAdditionalInformation>(gameComponent),
                GameComponentType.Health => await TryDeleteComponent<CharacterHealth>(gameComponent),
                GameComponentType.CharacterItem => await TryDeleteComponent<CharacterItem>(gameComponent),
                GameComponentType.Profession => await TryDeleteComponent<CharacterProfession>(gameComponent),
                GameComponentType.Trait => await TryDeleteComponent<CharacterTrait>(gameComponent),
                GameComponentType.Card => await TryDeleteComponent<CharacterCard>(gameComponent),
                GameComponentType.ExternalSurrounding => await TryDeleteComponent<GameExternalSurrounding>(gameComponent),
                _ => false,
            };
        }
        public async Task<IEnumerable<IGameComponent>> GetComponents(GameComponentsQuery gameComponentsQuery)
        {
            var gameComponentType = gameComponentsQuery.GameComponentType;
            return gameComponentType switch
            {
                GameComponentType.Catastrophe => await GetComponentsFromRepository<GameCatastrophe>(gameComponentsQuery),
                GameComponentType.BunkerEnviroment => await GetComponentsFromRepository<BunkerEnviroment>(gameComponentsQuery),
                GameComponentType.BunkerWall => await GetComponentsFromRepository<BunkerWall>(gameComponentsQuery),
                GameComponentType.ItemBunker => await GetComponentsFromRepository<ItemBunker>(gameComponentsQuery),
                GameComponentType.BunkerObject => await GetComponentsFromRepository<BunkerObject>(gameComponentsQuery),
                GameComponentType.Phobia => await GetComponentsFromRepository<CharacterPhobia>(gameComponentsQuery),
                GameComponentType.Hobby => await GetComponentsFromRepository<CharacterHobby>(gameComponentsQuery),
                GameComponentType.AdditionalInformation => await GetComponentsFromRepository<CharacterAdditionalInformation>(gameComponentsQuery),
                GameComponentType.Health => await GetComponentsFromRepository<CharacterHealth>(gameComponentsQuery),
                GameComponentType.CharacterItem => await GetComponentsFromRepository<CharacterItem>(gameComponentsQuery),
                GameComponentType.Profession => await GetComponentsFromRepository<CharacterProfession>(gameComponentsQuery),
                GameComponentType.Trait => await GetComponentsFromRepository<CharacterTrait>(gameComponentsQuery),
                GameComponentType.ExternalSurrounding => await GetComponentsFromRepository<GameExternalSurrounding>(gameComponentsQuery),
                GameComponentType.Card => await GetComponentsFromRepository<CharacterCard>(gameComponentsQuery),
                _ => Enumerable.Empty<IGameComponent>(),
            };
        }
        public async Task SaveChanges()
        {
            await unitOfWork.Save(default);
        }
        private async Task AddComponentToRepository<T>(T component) where T : class, IGameComponent
        {
            await gameComponentsRepository.AddComponent(component);
        }
        private async Task<ComponentId> GetComponentIdAsync(GameComponentType gameComponentType)
        {
            var components = await GetComponents(new GameComponentsQuery(gameComponentType, 0, int.MaxValue));
            var maxId = components.MaxBy(c => c.Id.Id);
            return new ComponentId(maxId?.Id.Id + 1 ?? 1);
        }
        private async Task<bool> TryDeleteComponent<T>(IGameComponent gameComponent) where T : class, IGameComponent
        {
            if (gameComponent is T component)
            {
                await gameComponentsRepository.RemoveComponent<T>(component);
                return true;
            }
            return false;
        }
        private async Task<IEnumerable<IGameComponent>> GetComponentsFromRepository<T>(GameComponentsQuery gameComponentsQuery) where T : class, IGameComponent
        {
            return await Task.Run(async ()=> 
            {
                return await gameComponentsRepository.GetComponents<T>(gameComponentsQuery.SkipCount, gameComponentsQuery.Count, false
                , c => c.Description.Contains(gameComponentsQuery.DescriptionQuery));
            });
        }
    }
}
