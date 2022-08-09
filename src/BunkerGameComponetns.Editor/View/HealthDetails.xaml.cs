using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor.View;

public partial class HealthDetails : ContentPage
{
	public HealthDetails(HealthDetailsViewModel healthDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = healthDetailsViewModel;
	}
}