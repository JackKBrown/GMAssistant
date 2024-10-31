using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GMAssistant.ViewModel;

public partial class CharacterGenViewModel : BaseViewModel
{
	public ObservableCollection<string> FullNames { get; set; } = new();
	List<NamePart> nameParts { get; set; }
	[ObservableProperty]
	List<BoolProperty> nameAncestries;
	FluffService fService;
	private int NumberOfNames = 20;
	public ObservableCollection<String> Characters { get; } = new();
	public CharacterGenViewModel(FluffService fluffService)
	{
		Title = "All Encounters Page";
		fService = fluffService;
		GetNamePartsAsync();
	}

	public async void GetNamePartsAsync()
	{
		nameParts = await fService.GetNameList();
		var ancestries = new List<string>(nameParts.Select(x => x.Ancestry).Distinct());
		NameAncestries = new List<BoolProperty>();
		foreach (var ancestry in ancestries)
		{
			NameAncestries.Add(new BoolProperty
			{
				Name = ancestry,
				Selected = true
			});
		}
		GenerateNames();
	}
	[RelayCommand]
	public void GenerateNames()
	{
		FullNames.Clear();
		List<NamePart> filteredNameParts = FilterNameParts();

		Random rng = new Random();
		List<NamePart> firstNames = filteredNameParts.Where(
			NP => NP.Position == 1).ToList();
		firstNames = firstNames.OrderBy(_ => rng.Next()).ToList();
		List<NamePart> lastNames = filteredNameParts.Where(
			NP => NP.Position == 2).ToList();
		lastNames = lastNames.OrderBy(_ => rng.Next()).ToList();
		for (int i = 0; i < NumberOfNames; i++)
		{
			if (firstNames.Count == i) { break; }
			string name = firstNames[i].Name + " " + lastNames[i].Name;
			FullNames.Add(name);
		}
	}
	public List<NamePart> FilterNameParts()
	{

		List<string> selectedAncestries = NameAncestries.Where(
			AC => AC.Selected).Select(AC => AC.Name).ToList();
		List<NamePart> filteredNameParts = nameParts.Where(
			NP => selectedAncestries.Contains(NP.Ancestry)).ToList();

		return filteredNameParts;
	}

	[RelayCommand]
	public void GetInfo()
	{

		Debug.WriteLine(FullNames.Count);
	}
}
