using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class AdditionalInformationDetails : ContentPage
{
	public AdditionalInformationDetails(AdditionalInformationDetailsViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}