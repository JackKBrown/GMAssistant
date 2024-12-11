using GMAssistant.Models;

namespace GMAssistant;

public static class Constants
{
	public const int SavedEncounterID = -1;
	public const string DatabaseFilename = "GMASQLite.db3";

	public const SQLite.SQLiteOpenFlags Flags =
		// open the database in read/write mode
		SQLite.SQLiteOpenFlags.ReadWrite |
		// create the database if it doesn't exist
		SQLite.SQLiteOpenFlags.Create |
		// enable multi-threaded database access
		SQLite.SQLiteOpenFlags.SharedCache;

	//C:\Users\jackk\AppData\Local\packages\com.companyname.gmassistant_9zz4h110yvjzm\LocalState
	public static string DatabasePath =>
		Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

	public static List<ShopType> ShopTypes = new List<ShopType>{
		new ShopType()
		{
			Name = "All",
			Tags = new List<string>() {},
			Image = "image.png"
		},
		new ShopType()
		{
			Name = "General",
			Tags = new List<string>() {"Adventuring Gear", "Trade Goods", "Animals and Gear","Materials", "Snares"},
			Image = "image.png"
		},
		new ShopType()
		{
			Name = "Hunter",
			Tags = new List<string>() {"Animals and Gear","Materials", "Snares"},
			Image = "image.png"
		},
		new ShopType()
		{
			Name = "Blacksmith",
			Tags = new List<string>() {"Armor", "Shields", "Runes", "Siege Weapons", "Weapons", "Consumables", "Customizations" },
			Image = "image.png"
		},
		new ShopType()
		{
			Name = "Magic",
			Tags = new List<string>() {"Runes", "Wands", "Tattoos", "Spellhearts", "Staves", "Grimoires", "Grafts", "Held Items", "Worn Items" },
			Image = "image.png"
		},
		new ShopType()
		{
			Name = "Apothecary",
			Tags = new List<string>() { "Alchemical Items", "Consumables"},
			Image = "image.png"
		},
		new ShopType()
		{
			Name = "Runesmith",
			Tags = new List<string>() {"Runes", "Tattoos", "Adjustments" },
			Image = "image.png"
		},
		new ShopType()
		{
			Name = "Esoterica",
			Tags = new List<string>() { "Cursed Items", "Tattoos", "Spellhearts", "Artifacts", "Assistive Items", "Held Items", "Other", "Relics"},
			Image = "image.png"
		},
	};
}
