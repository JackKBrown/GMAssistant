using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GMAssistant.Model
{
	public class Entity
	{
		[JsonIgnore]
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[JsonIgnore]
		public int Initiative { get; set; }
		[JsonIgnore]
		public int EncounterID { get; set; }


		public string Name { get; set; }
		public string Details { get; set; }
		public int MaxHP {  get; set; }
		public int CurrentHP { get; set; }
		public int AC { get; set; }
		public int Ref { get; set; }
		public int Fort { get; set; }
		public int Will { get; set; }
		public int InitBonus { get; set; }
		public string attacks { get; set; }
		public string abilities { get; set; }
		public string Conditions { get; set; }

	}
	[JsonSerializable(typeof(List<Entity>))]
	public sealed partial class EntityContext : JsonSerializerContext
	{

	}
}
