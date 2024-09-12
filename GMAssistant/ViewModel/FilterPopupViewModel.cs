using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;

namespace GMAssistant.ViewModel;

public partial class FilterPopupViewModel : BaseViewModel
{
	[ObservableProperty]
	public FilterPreferences preferences;

	[ObservableProperty]
	public int editable = 0;

	public FilterPopupViewModel()
	{
		Title = "All Encounters Page";
	}

	[RelayCommand]
	public async Task SaveChangesAsync()
	{
		Preferences.MaxCR = Editable;
	}

}
