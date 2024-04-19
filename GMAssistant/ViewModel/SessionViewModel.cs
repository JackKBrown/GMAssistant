

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using GMAssistant.View;

namespace GMAssistant.ViewModel;

[QueryProperty(nameof(CurrentSession), "session")]
public partial class SessionViewModel:BaseViewModel
{
	GMADatabase db;

	[ObservableProperty]
	public Session currentSession;
	public ObservableCollection<Encounter> Encounters { get; set; } = new();
	public SessionViewModel(GMADatabase database)
	{
		Title = "Session";
		db = database;
	}

	[RelayCommand]
	async Task NewEncounterAsync()
	{
		Encounter encounter = new Encounter
		{
			Name = "new Encounter",
			Description = "Description",
			SessionID = CurrentSession.ID
		};
		await db.SaveEncounterAsync(encounter);
		await GoToEncounterAsync(encounter);
	}

	[RelayCommand]
	async Task GoToEncounterAsync(Encounter encounter)
	{
		if (encounter == null) { return; }
		await Shell.Current.GoToAsync($"{nameof(EncounterPage)}", true,
			new Dictionary<string, object>
			{
					{"encounter", encounter }
			});

	}
	public async Task SaveSession()
	{
		try
		{
			await db.SaveSessionAsync(CurrentSession);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("error", $"Session error{ex.Message}", "ok");
		}

	}
	
	public async Task GetEncounters()
	{
		if(CurrentSession == null) { return; }
		if (IsBusy) return;

		try
		{
			IsBusy = true;
			var sessionsTemp = await db.GetSessionEncounters(CurrentSession.ID);
			Debug.WriteLine($"SESSION ID is {sessionsTemp.Count}");
			if (Encounters.Count != 0)
				Encounters.Clear();

			foreach (var session in sessionsTemp)
				Encounters.Add(session);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("error", $"Session error{ex.Message}", "ok");
		}
		finally { IsBusy = false; }
	}
	[RelayCommand]
	async Task Displayinfo()
	{
		if (CurrentSession == null)
		{
			await Shell.Current.DisplayAlert("error", $"current session is null", "ok");
		}
		else
		{
			await Shell.Current.DisplayAlert("error", $"current session is not null {CurrentSession.Name}", "ok");

		}
	}
}
