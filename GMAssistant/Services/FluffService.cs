using GMAssistant.Model;
using System.Text.Json;

namespace GMAssistant.Services;

public class FluffService
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
	public List<NamePart> nameList;
	public async Task<List<NamePart>> GetNameList()
	{
		if (detailList?.Count >= 0)
			return nameList;

		using var stream = await FileSystem.OpenAppPackageFileAsync("Names.json");
		using var reader = new StreamReader(stream);
		var contents = await reader.ReadToEndAsync();
		nameList = JsonSerializer.Deserialize(contents, NamePartContext.Default.ListNamePart);
		return nameList;
	}

}

