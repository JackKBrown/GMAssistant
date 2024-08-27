

using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Model;
using GMAssistant.Services;
using GMAssistant.View;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;

namespace GMAssistant.ViewModel
{
	public partial class ItemsViewModel : BaseViewModel
	{
		GMADatabase database;
		ItemService itemService;
		public ObservableRangeCollection<Item> ItemResults { get; } = new();
		public List<Item> items = new();
		public List<Item> itemsFiltered = new();
		int _pageSize = 20;
		public ItemsViewModel(GMADatabase dataBase, ItemService iService)
		{
			Title = "All Encounters Page";
			database = dataBase;
			itemService = iService;
			GetItems();
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
				ItemResults.AddRange(itemsFiltered.Take(_pageSize));
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
			itemsFiltered=items.Where(
				Item => Item.Level.Contains("6")).ToList();
			ItemResults.AddRange(itemsFiltered.Take(_pageSize));

		}
	}
}
