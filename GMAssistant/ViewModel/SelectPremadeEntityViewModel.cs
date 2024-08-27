﻿using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Model;
using GMAssistant.Services;
using MvvmHelpers;
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

	//Filter options
	[ObservableProperty]
	public int maxLevel = 30;
	[ObservableProperty]
	public int minLevel = 1;

	[ObservableProperty]
	public string trait = "";

	// might not need to load this if it instead returns the details?
	readonly GMADatabase db;
	readonly BestiaryService bService;
	private readonly IPopupService pService;
	[ObservableProperty]
	public int encounterid;
	[ObservableProperty]
	public string searchQuery;

	public FilterPreferences FilterPreferences { get; } = new();
	public ObservableRangeCollection<Entity> BestiaryResults { get; } = new();
	public List<Entity> bestiary = new();
	public List<Entity> filteredBestiary = new();
	int _pageSize = 20;
	public SelectPremadeEntityViewModel (GMADatabase database, BestiaryService bestiaryService, IPopupService popupService)
	{
		Title = "Load from Bestiary";
		db = database;
		bService = bestiaryService;
		pService = popupService;
		GetNextBestiaryAsync();
	}

	[RelayCommand]
	public async void GetBestiaryAsync()
	{
		if (BestiaryResults.Count == 0)
		{
			bestiary = await bService.GetPathfinderBestiary();
			filteredBestiary = bestiary;
			BestiaryResults.AddRange(filteredBestiary.Take(_pageSize));
		}
	}
	[RelayCommand]
	public async void GetNextBestiaryAsync()
	{
		if (BestiaryResults.Count > 0)
		{
			BestiaryResults.AddRange(filteredBestiary.Skip(BestiaryResults.Count).Take(_pageSize));
		}
		else
		{
			GetBestiaryAsync();
		}
	}


	[RelayCommand]
	public async void Filter()
	{
		List<Entity> bestiaryTemp = bestiary.Where(
			Entity => Entity.Name.ToLower().Contains(SearchQuery.ToLower())).ToList();

		bestiaryTemp = bestiaryTemp.Where(
			Entity => (Entity.Level < maxLevel && Entity.Level > MinLevel)).ToList();

		if (Trait != "")
		{
			bestiaryTemp = bestiaryTemp.Where(
				Entity => Entity.Traits.ToLower().Contains(Trait.ToLower())).ToList();
		}

		filteredBestiary = bestiaryTemp;
		if (BestiaryResults.Count > 0)
			BestiaryResults.Clear();
		BestiaryResults.AddRange(filteredBestiary.Skip(BestiaryResults.Count).Take(_pageSize));
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
	public async Task FilterPopupAsync()
	{
		await pService.ShowPopupAsync<FilterPopupViewModel>(onPresenting:
			ViewModel => {
				ViewModel.Preferences = FilterPreferences;
			});
	}
}
