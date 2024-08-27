using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using GMAssistant.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
