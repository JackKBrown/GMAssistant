﻿using GMAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GMAssistant.Services
{
    public class ItemService
    {
		public List<Item> itemList;
		public async Task<List<Item>> GetPathfinderItems()
		{
			if (itemList?.Count >= 0)
				return itemList;

			using var stream = await FileSystem.OpenAppPackageFileAsync("PathfinderItems.json");
			using var reader = new StreamReader(stream);
			var contents = await reader.ReadToEndAsync();
			itemList = JsonSerializer.Deserialize(contents, ItemContext.Default.ListItem);
			return itemList;
		}
	}
}