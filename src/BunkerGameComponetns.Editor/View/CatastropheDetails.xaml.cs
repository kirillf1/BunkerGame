using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class CatastropheDetails : ContentPage
{
	public CatastropheDetails(CatastropheDetailsViewModel detailsViewModel)
	{
		InitializeComponent();
		base.BindingContext = detailsViewModel;
	}
}