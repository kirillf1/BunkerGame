using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class BunkerItemDetails : ContentPage
{
	public BunkerItemDetails(BunkerItemDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}