using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class BunkerObjectDetails : ContentPage
{
	public BunkerObjectDetails(BunkerObjectDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}