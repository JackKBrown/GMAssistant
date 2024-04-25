using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class SelectPremadeEntity : ContentPage
{
	SelectPremadeEntityViewModel ViewModel;
	public SelectPremadeEntity(SelectPremadeEntityViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		ViewModel = viewModel;
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		_ = ViewModel.GetBestiaryAsync();
	}
}