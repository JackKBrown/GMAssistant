

using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using GMAssistant.View;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GMAssistant.ViewModel
{
	public partial class AllSessionsViewModel : BaseViewModel
	{
		GMADatabase database;
		public ObservableCollection<Session> Sessions { get; } = new();
		public AllSessionsViewModel(GMADatabase dataBase)
		{
			Title = "All Sessions Page";
			database = dataBase;
			_ = GetSessionsAsync();
			Debug.WriteLine(Constants.DatabasePath);
		}
		[RelayCommand]
		public async Task NewSessionAsync()
		{
			Session session = new Session
			{
				Name = "new session",
				Description = "Description"
			};
			await database.SaveSessionAsync(session);
			await GoToSessionAsync(session);
		}
		[RelayCommand]
		public async Task DeleteSessionAsync(Session session)
		{
			if (session == null) { return; }
			_ = await database.DeleteSessionAsync(session);
			Sessions.Remove(session);
		}

		[RelayCommand]
		public async Task GoToSessionAsync(Session session)
		{
			if (session == null) { return; }
			await Shell.Current.GoToAsync($"{nameof(SessionPage)}", true,
				new Dictionary<string, object>
				{
					{"session", session }
				});

		}

		[RelayCommand]
		public async Task GetSessionsAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var sessionsTemp = await database.GetSessionsAsync();

				if (Sessions.Count != 0)
					Sessions.Clear();

				foreach (var session in sessionsTemp)
					Sessions.Add(session);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				await Shell.Current.DisplayAlert("error", $"Session error{ex.Message}", "ok");
			}
			finally { IsBusy = false; }
		}
	}
}
