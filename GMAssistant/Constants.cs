namespace GMAssistant
{
	public static class Constants
	{
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
	}
}
