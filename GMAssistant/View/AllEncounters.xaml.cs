using GMAssistant.ViewModel;
using System.Diagnostics;

namespace GMAssistant.View;

public partial class AllEncounters : ContentPage
{
	public AllEncounters(AllEncountersViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		var vm = BindingContext as AllEncountersViewModel;
		Debug.WriteLine($"old count is {vm.Encounters.Count}");
		_ = vm.GetEncountersAsync();
	}
}