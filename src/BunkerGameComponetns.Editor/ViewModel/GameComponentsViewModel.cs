using BunkerGameComponetns.Editor.Model;
using BunkerGameComponetns.Editor.Services;

namespace BunkerGameComponetns.Editor.ViewModel
{
    public partial class GameComponentsViewModel : BaseViewModel
    {
        public bool IsNotBusy => !IsBusy;
        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy),nameof(CanSave))]
        private bool isBusy;
        public List<GameComponentType> ComponentTypes { get; }
        [ObservableProperty]
        private GameComponentType gameComponentType;
        [ObservableProperty]
        private string descriptionQuery;
        
        
        public bool CanSave => IsNotBusy && (GameComponents?.Any() ?? false); 
        private readonly GameComponentsService gameComponentsService;
        public ObservableRangeCollection<IGameComponent> GameComponents { get; }
        public GameComponentsViewModel(GameComponentsService gameComponentsService)
        {
            GameComponentType = GameComponentType.AdditionalInformation;
            Title = GameComponentType.ToString();
            GameComponents = new();
            ComponentTypes = new(Enum.GetValues(typeof(GameComponentType)).Cast<GameComponentType>());
            DescriptionQuery = string.Empty;
            this.gameComponentsService = gameComponentsService;
        }

        
        [ICommand]
        private async Task GoToDetailsAsync(IGameComponent gameComponent)
        {
            await gameComponentsService.NavigateToDetails(gameComponent);
        }
        [ICommand]
        private async Task SaveChanges()
        {
            IsBusy = true;
            try
            {
                await gameComponentsService.SaveChanges();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [ICommand]
        private async Task AddComponent()
        {
            IsBusy = true;
            try
            {
                var newComponent = await gameComponentsService.AddEmptyComponent(GameComponentType);
                GameComponents.Insert(0, newComponent);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [ICommand]
        private async Task RemoveComponent(IGameComponent gameComponent)
        {
            IsBusy = true;
            try
            {
                GameComponents.Remove(gameComponent);
                await gameComponentsService.TryDeleteComponentByGameComponentType(GameComponentType, gameComponent);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [ICommand]
        private async Task GetComponents()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                GameComponents.Clear();
                var components = await gameComponentsService.GetComponents(
                    new GameComponentsQuery(GameComponentType, 0, int.MaxValue, DescriptionQuery));
                GameComponents.AddRange(components);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
