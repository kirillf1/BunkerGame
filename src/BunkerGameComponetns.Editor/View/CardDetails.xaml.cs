using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class CardDetails : ContentPage
{
	public CardDetails(CardDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}