using System.Text.Json.Serialization;

namespace GMAssistant.Model;

public class NamePart
{
	public string Name { get; set; }
	public int Position { get; set; }
	public string Ancestry { get; set; }
}
[JsonSerializable(typeof(List<NamePart>))]
public sealed partial class NamePartContext : JsonSerializerContext
{

}
