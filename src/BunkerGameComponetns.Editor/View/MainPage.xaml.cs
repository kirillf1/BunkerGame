using BunkerGameComponetns.Editor.ViewModel;

namespace BunkerGameComponetns.Editor
{
    public partial class MainPage : ContentPage
    {
        private readonly GameComponentsViewModel gameComponentsViewModel;

        public MainPage(GameComponentsViewModel gameComponentsViewModel)
        {
            InitializeComponent();
            this.gameComponentsViewModel = gameComponentsViewModel;
            this.BindingContext = gameComponentsViewModel;
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();
            var gameComponents = gameComponentsViewModel.GameComponents;
            var gameComponentArr = gameComponents.ToArray();
            gameComponents.Clear();
            gameComponents.AddRange(gameComponentArr);
        }

        private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            await gameComponentsViewModel?.GetComponentsCommand?.ExecuteAsync(null);
        }
    }
}