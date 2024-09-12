using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class AllSessions : ContentPage
{
	AllSessionsViewModel ViewModel;
	public AllSessions(AllSessionsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		ViewModel = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		_ = ViewModel.GetSessionsAsync();
	}

	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);
	}
}