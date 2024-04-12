using GMAssistant.ViewModel;
using System.Diagnostics;

namespace GMAssistant.View;

public partial class SessionPage : ContentPage
{
	public SessionPage(SessionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		var vm = BindingContext as SessionViewModel;
		Debug.WriteLine($"SESSION ID is {vm.currentSession}");
		_ = vm.GetEncounters();
	}
}