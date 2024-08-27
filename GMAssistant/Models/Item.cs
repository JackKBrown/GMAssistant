using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GMAssistant.Model
{
    public class Item
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Cost { get; set; }
        public string Level { get; set; }
        public string Rarity { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Usage { get; set; }
        public string Bulk { get; set; }
        public string Source { get; set; }
        public string Spoiler { get; set; }
        public string PFS { get; set; }
    }
	[JsonSerializable(typeof(List<Item>))]
	public sealed partial class ItemContext : JsonSerializerContext
	{

	}
}
