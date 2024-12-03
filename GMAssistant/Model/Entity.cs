using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace GMAssistant.Model
{
	public enum EntityType
	{
		Ally = 0,
		Enemy = 1,
		Neutral = 2,
		Hazard = 3
	}
	public class Entity : INotifyPropertyChanged
	{
		//Not in bestiary file
		[JsonIgnore]
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		private int _initiative;
		[JsonIgnore]
		public int Initiative
		{
			get => _initiative;
			set
			{
				if (_initiative != value)
				{
					_initiative = value;
					OnPropertyChanged();  // Notify UI about the change
				}
			}
		}
		[JsonIgnore]
		public int EncounterID { get; set; }
		[JsonIgnore]
		public string Details { get; set; }
		private string _conditions;
		[JsonIgnore]
		public string Conditions
		{
			get => _conditions;
			set
			{
				if (_conditions != value)
				{
					_conditions = value;
					OnPropertyChanged();  // Notify UI about the change
				}
			}
		}

		private int _currentHP;
		[JsonIgnore]
		public int CurrentHP
		{
			get => _currentHP;
			set
			{
				if (_currentHP != value)
				{
					_currentHP = value;
					OnPropertyChanged();  // Notify UI about the change
				}
			}
		}
		[JsonIgnore]
		public EntityType EType { get; set; }


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

		[JsonIgnore]
		public List<string> TraitList
		{
			get
			{
				if (!string.IsNullOrEmpty(Traits))
					return Traits.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
				else return new List<string>();
			}
		}

		[JsonIgnore]
		public List<string> ActionList
		{
			get
			{
				if (!string.IsNullOrEmpty(Actions))
					return Actions.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
				else return new List<string>();
			}
		}

		[JsonIgnore]
		public List<string> SpellList
		{
			get
			{
				if (!string.IsNullOrEmpty(Spells))
					return Spells.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
				else return new List<string>();
			}
		}

		[JsonIgnore]
		public List<string> AllList
		{
			get
			{
				return new List<string>(ActionList.Concat(SpellList));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public Entity Clone()
		{
			Entity clone = (Entity)MemberwiseClone();
			clone.ID = 0;
			return clone;
		}
	}

	[JsonSerializable(typeof(List<Entity>))]
	public sealed partial class EntityContext : JsonSerializerContext
	{

	}
}
