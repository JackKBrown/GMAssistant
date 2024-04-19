using GMAssistant.ViewModel;
using System.Diagnostics;

namespace GMAssistant.View;

public partial class SessionPage : ContentPage
{
	SessionViewModel ViewModel;
	public SessionPage(SessionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		ViewModel = viewModel;

	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		Debug.WriteLine($"SESSION ID is {ViewModel.CurrentSession}");
		_ = ViewModel.GetEncounters();
	}

	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);
		_ = ViewModel.SaveSession();
	}
}