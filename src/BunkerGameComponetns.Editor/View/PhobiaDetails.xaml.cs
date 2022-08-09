using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class PhobiaDetails : ContentPage
{
	public PhobiaDetails(PhobiaDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}