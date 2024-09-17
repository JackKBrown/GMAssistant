using System.Text.Json.Serialization;

namespace GMAssistant.Models
{
	public class Detail
	{
		public string Name { get; set; }
	}
	[JsonSerializable(typeof(List<Detail>))]
	public sealed partial class DetailContext : JsonSerializerContext
	{

	}
}
