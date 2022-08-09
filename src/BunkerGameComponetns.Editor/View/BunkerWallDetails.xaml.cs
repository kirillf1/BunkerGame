using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class BunkerWallDetails : ContentPage
{
	public BunkerWallDetails(BunkerWallDetailsViewModel viewModel)
	{
		InitializeComponent();
		base.BindingContext = viewModel;
		
    }
}