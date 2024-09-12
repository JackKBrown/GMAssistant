using SQLite;
using System.Text.Json.Serialization;

namespace GMAssistant.Model
{
	public enum EntityType
	{
		PLAYER,
		ENEMY,
		HAZARD
	}
	public class Entity
	{
		//Not in bestiary file
		[JsonIgnore]
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[JsonIgnore]
		public int Initiative { get; set; }
		[JsonIgnore]
		public int EncounterID { get; set; }
		[JsonIgnore]
		public string Details { get; set; }
		[JsonIgnore]
		public string Conditions { get; set; }
		[JsonIgnore]
		public int CurrentHP { get; set; }
		[JsonIgnore]
		public EntityType EntityType { get; set; }


		//In the bestiry file
		public int Level { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }
		public string family { get; set; }
		public string Source { get; set; }
		public string Rarity { get; set; }
		public string Size { get; set; }
		public string Speed { get; set; }
		[JsonPropertyName("HP")]
		public int MaxHP { get; set; }
		public int AC { get; set; }
		public int Ref { get; set; }
		public int Fort { get; set; }
		public int Will { get; set; }
		public int Perception { get; set; }
		public string Traits { get; set; }
		public string Actions { get; set; }
		public string Spells { get; set; }
		public string RIW { get; set; }

		// methods
		public List<string> TraitList()
		{
			if (string.IsNullOrEmpty(Traits))
				return Traits.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
			else return new List<string>();
		}
		public List<string> ActionList()
		{
			if (string.IsNullOrEmpty(Actions))
				return Actions.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
			else return new List<string>();
		}
		public List<string> SpellList()
		{
			if (string.IsNullOrEmpty(Spells))
				return Spells.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
			else return new List<string>();
		}
	}

	[JsonSerializable(typeof(List<Entity>))]
	public sealed partial class EntityContext : JsonSerializerContext
	{

	}
}
