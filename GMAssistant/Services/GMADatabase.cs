using GMAssistant.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GMAssistant.Services
{
	public class GMADatabase
	{
		SQLiteAsyncConnection Database;

		public async Task Init()
		{
			if (Database is not null)
				return;

			Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
			var sessionRes = await Database.CreateTableAsync<Session>();
			var encounterRes = await Database.CreateTableAsync<Encounter>();
			var entityRes = await Database.CreateTableAsync<Entity>();
		}

		//SESSION
		public async Task<List<Session>> GetSessionsAsync()
		{
			await Init();
			return await Database.Table<Session>().ToListAsync();
		}

		public async Task<int> SaveSessionAsync(Session item)
		{
			await Init();
			if (item.ID != 0)
			{
				return await Database.UpdateAsync(item);
			}
			else
			{
				return await Database.InsertAsync(item);
			}
		}

		public async Task<List<Encounter>> GetSessionEncounters(int ID)
		{
			await Init();
			Debug.WriteLine($"SESSION ID is {ID}");
			return await Database.QueryAsync<Encounter>("select * from Encounter where SessionID=?", ID);
		}

		//ENCOUNTER
		public async Task<List<Encounter>> GetEncountersAsync()
		{
			await Init();
			return await Database.Table<Encounter>().ToListAsync();
		}
		public async Task<int> SaveEncounterAsync(Encounter item)
		{
			await Init();
			if (item.ID != 0)
			{
				return await Database.UpdateAsync(item);
			}
			else
			{
				return await Database.InsertAsync(item);
			}
		}

		public async Task<List<Entity>> GetEncounterEnitities(int ID)
		{
			await Init();
			Debug.WriteLine($"Encounter ID is {ID}");
			return await Database.QueryAsync<Entity>("select * from Entity where EncounterID=?", ID);
		}
		//ENTITY
		public async Task<List<Entity>> GetEntitysAsync()
		{
			await Init();
			return await Database.Table<Entity>().ToListAsync();
		}
		public async Task<int> SaveEntitysAsync(Entity item)
		{
			await Init();
			if (item.ID != 0)
			{
				return await Database.UpdateAsync(item);
			}
			else
			{
				return await Database.InsertAsync(item);
			}
		}

		public async Task<int> DeleteEntityAsync(Entity item)
		{
			await Init();
			if (item.ID != 0)
			{
				return await Database.DeleteAsync(item);
			}
			return 0;
		}
	}
}
