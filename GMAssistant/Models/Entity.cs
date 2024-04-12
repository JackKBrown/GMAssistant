using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAssistant.Model
{
	public class Entity
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int MaxHP {  get; set; }
		public int CurrentHP { get; set; }
		public int Initiative { get; set; }
		public string Conditions { get; set; }
		public int EncounterID { get; set; }
	}
}
