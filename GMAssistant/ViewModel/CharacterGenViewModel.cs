using GMAssistant.Services;
using System.Collections.ObjectModel;

namespace GMAssistant.ViewModel;

public partial class CharacterGenViewModel : BaseViewModel
{
	GMADatabase database;
	public ObservableCollection<String> Characters { get; } = new();
	public CharacterGenViewModel(GMADatabase dataBase)
	{
		Title = "All Encounters Page";
		database = dataBase;
	}
}
