using GMAssistant.Models;
using System.Text.Json;

namespace GMAssistant.Services;

public class ShopDetailsService
{
	public List<Detail> detailList;
	public async Task<List<Detail>> GetDetailsAsync()
	{
		if (detailList?.Count >= 0)
			return detailList;

		using var stream = await FileSystem.OpenAppPackageFileAsync("PathfinderBestiary.json");
		using var reader = new StreamReader(stream);
		var contents = await reader.ReadToEndAsync();
		detailList = JsonSerializer.Deserialize(contents, DetailContext.Default.ListDetail);
		return detailList;
	}
}

