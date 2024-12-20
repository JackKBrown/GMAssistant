using GMAssistant.ViewModel;
using System.Diagnostics;

namespace GMAssistant.View;

public partial class EncounterPage : ContentPage
{
	EncounterViewModel ViewModel;
	public EncounterPage(EncounterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		ViewModel = viewModel;
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		_ = ViewModel.GetEntities();
		ViewModel.SortEntities();
	}

	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);
		_ = ViewModel.SaveEncounter();
		_ = ViewModel.SaveEntitiesAsync();
	}

	private void Entry_TextChanged(object sender, TextChangedEventArgs e)
	{
		ViewModel.SortEntities();
	}

	private void MaxHP_Updated(object sender, EventArgs e)
	{
		Debug.WriteLine(sender);
		Debug.WriteLine(sender.ToString());

		//ViewModel.MaxHPUpdated();
	}
}