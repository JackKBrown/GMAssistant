

using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using GMAssistant.View;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GMAssistant.ViewModel
{
	public partial class AllEncountersViewModel:BaseViewModel
	{
		GMADatabase database;
		public ObservableCollection<Encounter> Encounters { get; } = new(); 
		public AllEncountersViewModel(GMADatabase dataBase)
		{
			Title = "All Encounters Page";
			database = dataBase;
		}

		[RelayCommand]
		async Task NewEncounterAsync()
		{
			Encounter encounter = new Encounter
			{
				Name = "new session",
				Description = "Description"
			};
			await database.SaveEncounterAsync(encounter);
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

		[RelayCommand]
		public async Task GetEncountersAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var encountersTemp = await database.GetEncountersAsync();

				if (Encounters.Count != 0)
					Encounters.Clear();

				foreach(var encounter in encountersTemp)
					Encounters.Add(encounter);
			}
			catch (Exception ex) 
			{
				Debug.WriteLine(ex);
				await Shell.Current.DisplayAlert("error", $"Session error{ex.Message}", "ok");
			}
			finally { IsBusy = false; }
		}
	}
}
