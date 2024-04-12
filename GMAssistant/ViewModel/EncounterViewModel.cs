

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GMAssistant.ViewModel;

[QueryProperty(nameof(CurrentEncounter), "encounter")]
public partial class EncounterViewModel : BaseViewModel
{
	GMADatabase db;
	[ObservableProperty]
	public Encounter currentEncounter;
	public ObservableCollection<Entity> Entities { get; set; } = new();
	public EncounterViewModel(GMADatabase dataBase)
	{
		Title = "Encounters Page";
		db = dataBase;
	}

	[RelayCommand]
	async Task NewEnitityAsync()
	{
		if (currentEncounter == null) { return; }
		Entity entity = new Entity
		{
			Name = "New Actor",
			Description = "Description",
			CurrentHP = 1,
			MaxHP = 1,
			Conditions = "",
			Initiative = 0,
			EncounterID = currentEncounter.ID,
		};
		await db.SaveEntitysAsync(entity);
		Entities.Add(entity);
	}
	public async Task GetEntities()
	{
		if (currentEncounter == null) { return; }
		if (IsBusy) return;

		try
		{
			IsBusy = true;
			var entityTemp = await db.GetEncounterEnitities(currentEncounter.ID);
			Debug.WriteLine($"SESSION ID is {entityTemp.Count}");
			if (Entities.Count != 0)
				Entities.Clear();

			foreach (var entity in entityTemp)
				Entities.Add(entity);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("error", $"Encounter error{ex.Message}", "ok");
		}
		finally { IsBusy = false; }
	}
}
