using GMAssistant.ViewModel;
using System.Diagnostics;

namespace GMAssistant.View;

public partial class EncounterPage : ContentPage
{
	public EncounterPage(EncounterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		var vm = BindingContext as EncounterViewModel;
		Debug.WriteLine($"SESSION ID is {vm.currentEncounter}");
		_ = vm.GetEntities();
	}
}