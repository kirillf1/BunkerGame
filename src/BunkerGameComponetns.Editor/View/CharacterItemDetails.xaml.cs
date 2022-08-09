using BunkerGameComponents.Domain;
using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;
public partial class CharacterItemDetails : ContentPage
{
    public CharacterItemDetails(CharacterItemDetailsViewModel viewModel)
    {
        InitializeComponent();
        base.BindingContext = viewModel;
    }
}