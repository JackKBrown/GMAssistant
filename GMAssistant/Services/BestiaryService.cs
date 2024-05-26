using GMAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GMAssistant.Services;

public class BestiaryService
{

	public List<Entity> pathfinderBestiaryList;
	public async Task<List<Entity>> GetPathfinderBestiary()
	{
		if (pathfinderBestiaryList?.Count >= 0) 
			return pathfinderBestiaryList;

		using var stream = await FileSystem.OpenAppPackageFileAsync("PathfinderBestiary.json");
		using var reader = new StreamReader(stream);
		var contents = await reader.ReadToEndAsync();
		pathfinderBestiaryList = JsonSerializer.Deserialize(contents, EntityContext.Default.ListEntity);
		return pathfinderBestiaryList;
	}
		
}
