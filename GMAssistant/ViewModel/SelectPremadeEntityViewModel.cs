using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

	//Filter options
	[ObservableProperty]
	ObservableCollection<BoolProperty> traits;

	[ObservableProperty]
	public int[] levels;
	[ObservableProperty]
	public int minLevelIndex;
	[ObservableProperty]
	public int maxLevelIndex;

	public FilterPreferences FilterPreferences { get; } = new();
	public ObservableRangeCollection<Entity> BestiaryResults { get; } = new();
	public List<Entity> bestiary = new();
	public List<Entity> filteredBestiary = new();
	int _pageSize = 20;
	public SelectPremadeEntityViewModel(GMADatabase database, BestiaryService bestiaryService, IPopupService popupService)
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
		if (bestiary.Count == 0)
		{
			bestiary = await bService.GetPathfinderBestiary();
			filteredBestiary = bestiary;

			//setup filter options
			var TempTraits = new List<string>(bestiary.SelectMany(x => x.TraitList).Distinct()).ToArray();
			Traits = new ObservableCollection<BoolProperty>();
			foreach (var trait in TempTraits)
				Traits.Add(new BoolProperty { Name = trait, Selected = true });
			// need to call the notify property changed here

			Levels = new List<int>(bestiary.Select(x => x.Level).Distinct().OrderDescending().Reverse()).ToArray();

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
		int maxLevel = Levels[MaxLevelIndex];
		int minLevel = Levels[MinLevelIndex];

		bestiaryTemp = bestiaryTemp.Where(
			Entity => (Entity.Level < maxLevel && Entity.Level > minLevel)).ToList();

		List<string> selectedTraits = Traits.Where(
			trait => trait.Selected).Select(AC => AC.Name).ToList();
		if (selectedTraits.Count > 0 || selectedTraits.Count == Traits.Count)
		{
			bestiaryTemp = bestiaryTemp.Where(
				Entity => (Entity.TraitList.Intersect(selectedTraits).ToList().Count > 0)).ToList();
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
			ViewModel =>
			{
				ViewModel.Preferences = FilterPreferences;
			});
	}

	[RelayCommand]
	public void SelectNone()
	{
		foreach (BoolProperty trait in Traits) trait.Selected = false;
	}

	[RelayCommand]
	public void SelectAll()
	{
		foreach (BoolProperty trait in Traits) trait.Selected = true;
	}
}
