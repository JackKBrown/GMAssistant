

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using MvvmHelpers;

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

		[ObservableProperty]
		public int[] shopLevels;
		[ObservableProperty]
		public string[] catagories;
		[ObservableProperty]
		public string[] subCatagories;
		[ObservableProperty]
		public string[] rarities;


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
			GetItems();
		}

		[RelayCommand]
		public async void GetItems()
		{
			if (items.Count == 0)
			{
				items = await itemService.GetPathfinderItems();

				//setup filter options
				Catagories = new List<string>(items.Select(x => x.Category).Distinct()).ToArray();
				SubCatagories = new List<string>(items.Select(x => x.Subcategory).Distinct()).ToArray();
				ShopLevels = new List<int>(items.Select(x => x.Level).Distinct()).ToArray();
				Rarities = new List<string>(items.Select(x => x.Rarity).Distinct()).ToArray();

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
				Item => Item.Level == 6).ToList();
			ItemResults.AddRange(itemsFiltered.Take(_pageSize));

		}
	}
}
