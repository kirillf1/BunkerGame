using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class TraitDetails : ContentPage
{
	public TraitDetails(TraitDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}