using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class ProfessionDetails : ContentPage
{
	public ProfessionDetails(ProfessionDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}