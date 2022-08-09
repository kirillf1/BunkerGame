using BunkerGame.GameTypes.GameComponentTypes;
using BunkerGameComponents.Domain.Catastrophes;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    [QueryProperty(nameof(GameComponent), nameof(GameComponent))]
    public partial class CatastropheDetailsViewModel : BaseViewModel
    {

        [ObservableProperty]
        private GameCatastrophe gameComponent;

        public CatastropheDetailsViewModel()
        {
            Title = "Catastrophe Update";
            CatastropheTypes = new(Enum.GetValues(typeof(CatastropheType)).Cast<CatastropheType>());
        }
        public List<CatastropheType> CatastropheTypes { get; set; }
        
    }
}
