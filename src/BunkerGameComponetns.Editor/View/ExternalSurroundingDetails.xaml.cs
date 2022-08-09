using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class ExternalSurroundingDetails : ContentPage
{
	public ExternalSurroundingDetails(ExternalSurroundingDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}