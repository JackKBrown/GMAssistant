using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using MvvmHelpers;
using System.Diagnostics;

namespace GMAssistant.ViewModel;

[QueryProperty(nameof(Encounterid), "encounterid")]
public partial class SelectSavedEntityViewModel : BaseViewModel
{
	// might not need to load this if it instead returns the details?
	readonly GMADatabase db;
	private readonly IPopupService pService;
	[ObservableProperty]
	public int encounterid;

	public ObservableRangeCollection<Entity> SavedEntities { get; } = new();

	public SelectSavedEntityViewModel(GMADatabase database, IPopupService popupService)
	{
		Title = "Load from Saved";
		db = database;
		pService = popupService;
	}

	[RelayCommand]
	public async void GetSavedEntities()
	{
		if (IsBusy) return;

		try
		{
			IsBusy = true;
			var entityTemp = await db.GetEncounterEnitities(Constants.SavedEncounterID);
			Debug.WriteLine($"SESSION ID is {entityTemp.Count}");
			if (SavedEntities.Count != 0)
				SavedEntities.Clear();

			foreach (var entity in entityTemp)
			{
				SavedEntities.Add(entity);
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("error", $"Encounter error{ex.Message}", "ok");
		}
		finally { IsBusy = false; }
	}
	[RelayCommand]
	public async Task SelectedItemAsync(Entity entity)
	{
		if (entity == null) return;
		Entity newEntity = entity.Clone();
		newEntity.EncounterID = encounterid;
		await db.SaveEntitysAsync(newEntity);
		await Shell.Current.GoToAsync("..");
	}

	[RelayCommand]
	public async Task DeleteItemAsync(Entity entity)
	{
		if (IsBusy) return;

		try
		{
			IsBusy = true;
			_ = await db.DeleteEntityAsync(entity);
		}
		catch (Exception ex)
		{ Debug.WriteLine(ex); }
		finally
		{ IsBusy = false; }
	}
}
