using SQLite;

namespace GMAssistant.Model;

public class Session
{
	[PrimaryKey, AutoIncrement]
	public int ID { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
}