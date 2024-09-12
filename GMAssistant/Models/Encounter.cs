using SQLite;

namespace GMAssistant.Model
{
	public class Encounter
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		//same as session no foreign key
		public int SessionID { get; set; }
	}
}
