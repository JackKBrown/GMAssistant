

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GMAssistant.ViewModel
{
	public partial class ItemsViewModel : BaseViewModel
	{
		GMADatabase database;
		ItemService itemService;

		[ObservableProperty]
		public ObservableRangeCollection<Item> itemResults;
		public List<Item> items = new();
		public List<Item> itemsFiltered = new();
		int _pageSize = 20;

		[ObservableProperty]
		public int[] shopLevels;
		[ObservableProperty]
		public ObservableCollection<BoolProperty> catagories;
		[ObservableProperty]
		public ObservableCollection<BoolProperty> subCatagories;
		[ObservableProperty]
		public ObservableCollection<BoolProperty> rarities;


		[ObservableProperty]
		public int catagoryIndex;
		[ObservableProperty]
		public int minLevelIndex;
		[ObservableProperty]
		public int maxLevelIndex;
		public ItemsViewModel(GMADatabase dataBase, ItemService iService)
		{
			Title = "All Encounters Page";
			database = dataBase;
			itemService = iService;
			ItemResults = new ObservableRangeCollection<Item>();
			GetItemsAsync();
		}

		[RelayCommand]
		public async void GetItemsAsync()
		{
			if (items.Count == 0)
			{
				items = await itemService.GetPathfinderItems();

				//setup filter options
				var TempSubCatagories = new List<string>(items.Select(x => x.Subcategory).Distinct()).ToArray();
				SubCatagories = new ObservableRangeCollection<BoolProperty>();
				foreach (var subcat in TempSubCatagories)
					SubCatagories.Add(new BoolProperty { Name = subcat, Selected = true });
				// need to call the notify property changed here

				ShopLevels = new List<int>(items.Select(x => x.Level).Distinct()).ToArray();

				var TempCatagories = new List<string>(items.Select(x => x.Category).Distinct()).ToArray();
				Catagories = new ObservableRangeCollection<BoolProperty>();
				foreach (var cat in TempCatagories)
					Catagories.Add(new BoolProperty { Name = cat, Selected = true });

				var TempRarities = new List<string>(items.Select(x => x.Rarity).Distinct()).ToArray();
				Rarities = new ObservableRangeCollection<BoolProperty>();
				foreach (var rarity in TempRarities)
					Rarities.Add(new BoolProperty { Name = rarity, Selected = true });

				MinLevelIndex = 0;
				MaxLevelIndex = ShopLevels.Count() - 1;
				CatagoryIndex = -1;

				itemsFiltered = items;
				ItemResults.AddRange(itemsFiltered.Take(_pageSize));
			}
		}

		[RelayCommand]
		public void GetNextItems()
		{
			Debug.WriteLine("getting next items");
			if (ItemResults.Count > 0)
			{
				ItemResults.AddRange(itemsFiltered.Skip(ItemResults.Count).Take(_pageSize));
			}
			else
			{
				GetItemsAsync();
			}
		}

		[RelayCommand]
		public void FilterItems()
		{
			ItemResults.Clear();
			int minlevel = ShopLevels[MinLevelIndex];
			int maxlevel = ShopLevels[MaxLevelIndex];
			List<string> selectedCatagories = Catagories.Where(
				AC => AC.Selected).Select(AC => AC.Name).ToList();
			List<string> selectedSubCatagories = SubCatagories.Where(
				AC => AC.Selected).Select(AC => AC.Name).ToList();
			List<string> selectedRarities = Rarities.Where(
				AC => AC.Selected).Select(AC => AC.Name).ToList();

			itemsFiltered = items.Where(
				Item => Item.Level >= minlevel).ToList();
			itemsFiltered = itemsFiltered.Where(
				Item => Item.Level <= maxlevel).ToList();
			itemsFiltered = itemsFiltered.Where(
				Item => selectedCatagories.Contains(Item.Category)).ToList();
			itemsFiltered = itemsFiltered.Where(
				Item => selectedSubCatagories.Contains(Item.Subcategory)).ToList();
			itemsFiltered = itemsFiltered.Where(
				Item => selectedRarities.Contains(Item.Rarity)).ToList();
			ItemResults.AddRange(itemsFiltered.Take(_pageSize));

		}
	}
}
