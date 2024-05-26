using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Models;
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
	private readonly IPopupService pService;
	[ObservableProperty]
	public int encounterid;
	[ObservableProperty]
	public string searchQuery;

	public FilterPreferences FilterPreferences { get; } = new();
	public ObservableCollection<Entity> BestiaryResults { get; } = new();
	public List<Entity> Bestiary { get; } = new();
	public SelectPremadeEntityViewModel (GMADatabase database, BestiaryService bestiaryService, IPopupService popupService)
	{
		Title = "Load from Bestiary";
		db = database;
		bService = bestiaryService;
		pService = popupService;
	}

	[RelayCommand]
	public async Task GetBestiaryAsync()
	{
		var bestiaryTemp = await bService.GetPathfinderBestiary();
		if (Bestiary.Count > 0)
			Bestiary.Clear();
		if (BestiaryResults.Count > 0)
			BestiaryResults.Clear();

		foreach (var creature in bestiaryTemp)
		{
			Bestiary.Add(creature);
			BestiaryResults.Add(creature);
		}
			
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

	[RelayCommand]
	public async void Filter()
	{
		List<Entity> bestiaryTemp = Bestiary.Where(
			Entity => Entity.Name.ToLower().Contains(SearchQuery.ToLower())).ToList();
		if (BestiaryResults.Count > 0)
			BestiaryResults.Clear();

		foreach (var creature in bestiaryTemp)
		{
			BestiaryResults.Add(creature);
		}
		await Shell.Current.DisplayAlert("error", $"Session error{FilterPreferences.MaxCR}", "ok");
	}

	[RelayCommand]
	public async Task FilterPopupAsync()
	{
		await pService.ShowPopupAsync<FilterPopupViewModel>(onPresenting:
			ViewModel => {
				ViewModel.Preferences = FilterPreferences;
			});
	}
}
