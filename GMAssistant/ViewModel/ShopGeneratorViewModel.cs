using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace GMAssistant.ViewModel;
public partial class ShopGeneratorViewModel : BaseViewModel
{
	ShopDetailsService detailService;
	ItemService itemService;
	public ObservableRangeCollection<Item> ItemResults { get; } = new();
	public List<Item> items = new();
	public List<Item> itemsFiltered = new();
	int _pageSize = 20;

	public ObservableCollection<String> ShopTypes { get; set; }

	public ShopGeneratorViewModel(ShopDetailsService dService, ItemService iService)
	{
		Title = "All Encounters Page";
		detailService = dService;
		itemService = iService;
		//GetItems();
		ShopTypes = new ObservableCollection<string>
		{
			"General",
			"blacksmith",
			"Apothecary"
		};
	}

	[RelayCommand]
	public async Task GenerateShopAsync()
	{
		throw new NotImplementedException();
	}

	[RelayCommand]
	public async void GetItems()
	{
		if (ItemResults.Count == 0)
		{
			items = await itemService.GetPathfinderItems();
			itemsFiltered = items;
			//ItemResults.AddRange(itemsFiltered.Take(_pageSize));
		}
	}

	[RelayCommand]
	public void GetNextItems()
	{
		if (ItemResults.Count > 0)
		{
			ItemResults.AddRange(itemsFiltered.Skip(ItemResults.Count).Take(_pageSize));
		}
		else
		{
			GetItems();
		}
	}

	[RelayCommand]
	public void FilterItems()
	{
		ItemResults.Clear();
		itemsFiltered = items.Where(
			Item => Item.Level.Contains("6")).ToList();
		ItemResults.AddRange(itemsFiltered.Take(_pageSize));

	}
}
