using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class SelectSavedEntity : ContentPage
{
	SelectSavedEntityViewModel ViewModel;
	public SelectSavedEntity(SelectSavedEntityViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		ViewModel = viewModel;
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		//ViewModel.GetBestiaryAsync();
	}
}