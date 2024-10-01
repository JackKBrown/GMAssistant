using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Models;
using GMAssistant.Services;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace GMAssistant.ViewModel;
public partial class ShopGeneratorViewModel : BaseViewModel
{
	FluffService detailService;
	ItemService itemService;
	public ObservableRangeCollection<Item> ShopItems { get; } = new();
	public List<Item> items = new();
	public List<Item> itemsFiltered = new();
	int _pageSize = 20;

	public ObservableCollection<ShopType> ShopTypes { get; set; }
	[ObservableProperty]
	public int[] shopLevels;
	[ObservableProperty]
	public string[] shopCatagories;
	[ObservableProperty]
	public int typeIndex;
	[ObservableProperty]
	public int levelIndex;

	public ShopGeneratorViewModel(FluffService dService, ItemService iService)
	{
		Title = "All Encounters Page";
		detailService = dService;
		itemService = iService;
		GetItems();
		ShopTypes = new ObservableCollection<ShopType>(Constants.ShopTypes);
	}

	[RelayCommand]
	public async Task ViewItemAsync()
	{
		throw new NotImplementedException();
	}

	[RelayCommand]
	public async Task GenerateShopAsync()
	{
		FilterItems();
	}

	[RelayCommand]
	public async void GetItems()
	{
		items = await itemService.GetPathfinderItems();
		itemsFiltered = items;
		ShopCatagories = new List<string>(items.Select(x => x.Category).Distinct()).ToArray();
		ShopLevels = new List<int>(items.Select(x => x.Level).Distinct()).ToArray();
		LevelIndex = 8;
		TypeIndex = -1;
	}

	[RelayCommand]
	public void FilterItems()
	{
		ShopItems.Clear();
		int level = ShopLevels[LevelIndex];
		int MinLevel = level - 2;
		int MaxLevel = level + 2;
		itemsFiltered = items.Where(
			Item => (Item.Level < MaxLevel && Item.Level < MinLevel)).ToList();
		if (TypeIndex > 0)
		{
			ShopType type = ShopTypes[TypeIndex];
			itemsFiltered = itemsFiltered.Where(
				Item => type.Tags.Contains(Item.Category)).ToList();
		}
		Random rng = new Random();
		itemsFiltered = itemsFiltered.OrderBy(_ => rng.Next()).ToList();
		ShopItems.AddRange(itemsFiltered.Take(_pageSize));

	}
}
