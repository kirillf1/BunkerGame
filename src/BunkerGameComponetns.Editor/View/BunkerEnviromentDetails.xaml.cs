using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class BunkerEnviromentDetails : ContentPage
{
	public BunkerEnviromentDetails(BunkerEnviromentDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}