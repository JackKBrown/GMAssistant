using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GMAssistant.ViewModel;

[QueryProperty(nameof(Encounterid), "encounterid")]
public partial class SelectPremadeEntityViewModel : BaseViewModel
{
	// might not need to load this if it instead returns the details?
	readonly GMADatabase db;
	readonly BestiaryService bService;

	[ObservableProperty]
	public int encounterid;

	public ObservableCollection<Entity> Bestiary { get; } = new();
	public SelectPremadeEntityViewModel (GMADatabase database, BestiaryService bestiaryService)
	{
		Title = "Load from Bestiary";
		db = database;
		bService = bestiaryService;
	}

	[RelayCommand]
	public async Task GetBestiaryAsync()
	{
		var bestiaryTemp = await bService.GetPathfinderBestiary();
		if (Bestiary.Count > 0)
			Bestiary.Clear();

		foreach(var creature in bestiaryTemp)
			Bestiary.Add(creature);
	}

	[RelayCommand]
	public async Task SelectedItemAsync(Entity entity)
	{
		if (entity == null) return;
		Debug.WriteLine(encounterid);
		entity.EncounterID = encounterid;
		await db.SaveEntitysAsync(entity);
		await Shell.Current.GoToAsync("..");
	}
}
