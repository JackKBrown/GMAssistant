

using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using GMAssistant.View;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace GMAssistant.ViewModel;

[QueryProperty(nameof(CurrentEncounter), "encounter")]
public partial class EncounterViewModel : BaseViewModel
{
	GMADatabase db;
	[ObservableProperty]
	public Encounter currentEncounter;
	private readonly IPopupService popupService;
	public ObservableCollection<Entity> Entities { get; set; } = new();
	public EncounterViewModel(GMADatabase dataBase, IPopupService popupService)
	{
		Title = "Encounters Page";
		db = dataBase;
		this.popupService = popupService;
	}

	[RelayCommand]
	async Task NewEnitityAsync()
	{
		if (CurrentEncounter == null) { return; }
		Entity entity = new Entity
		{
			Name = "New Actor",
			Details = "Description",
			CurrentHP = 1,
			MaxHP = 1,
			Conditions = "",
			Initiative = 0,
			Perception = 0,
			AC=10,
			Fort=0,
			Ref=0,
			Will=0,
			Actions = "",
			EncounterID = CurrentEncounter.ID,
		};
		await db.SaveEntitysAsync(entity);
		Entities.Add(entity);
	}
	public async Task GetEntities()
	{
		if (CurrentEncounter == null) { return; }
		if (IsBusy) return;

		try
		{
			IsBusy = true;
			var entityTemp = await db.GetEncounterEnitities(CurrentEncounter.ID);
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

	public async Task SaveEncounter()
	{
		try
		{
			await db.SaveEncounterAsync(CurrentEncounter);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("error", $"Encounter error{ex.Message}", "ok");
		}

	}
	[RelayCommand]
	public async Task ShowInfoAsync(Entity entity)
	{
		await popupService.ShowPopupAsync<ExtraInfoViewModel>(onPresenting: 
			ViewModel  => {
				ViewModel.CurrrentEntity = entity;
			});
	}

	[RelayCommand]
	public void SortEntities()
	{
		var orderedEntities = Entities.OrderByDescending(o => o.Initiative).ToList();
		Entities.Clear();
		foreach (var entity in orderedEntities)
			Entities.Add(entity);
	}

	[RelayCommand]
	public async Task SaveEntitiesAsync()
	{
		foreach( Entity entity in Entities )
			await db.SaveEntitysAsync(entity);
	}

	[RelayCommand]
	public async Task GoToSelectEntityAsync()
	{
		Debug.WriteLine($"current encounter id is {CurrentEncounter.ID}");
		await Shell.Current.GoToAsync($"{nameof(SelectPremadeEntity)}?encounterid={CurrentEncounter.ID}");
	}
}
