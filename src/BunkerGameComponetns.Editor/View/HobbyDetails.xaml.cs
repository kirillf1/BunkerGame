using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class HobbyDetails : ContentPage
{
	public HobbyDetails(HobbyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}