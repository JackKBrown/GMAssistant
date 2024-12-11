using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using GMAssistant.View;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GMAssistant.ViewModel;

[QueryProperty(nameof(CurrentEncounter), "encounter")]
public partial class EncounterViewModel : BaseViewModel
{
	GMADatabase db;
	[ObservableProperty]
	public Encounter currentEncounter;

	[ObservableProperty]
	public int modify;

	[ObservableProperty]
	public bool descriptionExpanded;

	private readonly IPopupService popupService;
	public ObservableCollection<Entity> Entities { get; set; } = new();
	public EncounterViewModel(GMADatabase dataBase, IPopupService popupService)
	{
		Title = "Encounters Page";
		db = dataBase;
		this.popupService = popupService;
		modify = 0;
		descriptionExpanded = false;
	}

	[RelayCommand]
	public void SubModify(Entity entity)
	{
		entity.CurrentHP -= Modify;
		if (entity.CurrentHP < 0) { entity.CurrentHP = 0; }
	}

	[RelayCommand]
	public void AddModify(Entity entity)
	{
		entity.CurrentHP += Modify;
		if (entity.CurrentHP > entity.MaxHP) { entity.CurrentHP = entity.MaxHP; }
	}

	[RelayCommand]
	async Task NewEntityAsync()
	{
		if (CurrentEncounter == null) { return; }
		Entity entity = new Entity
		{
			Name = "New Actor",
			Details = "Description",
			CurrentHP = 1,
			MaxHP = 1,
			Conditions = "",
			Initiative = 0,
			Perception = 0,
			AC = 10,
			Fort = 0,
			Ref = 0,
			Will = 0,
			Actions = "",
			EncounterID = CurrentEncounter.ID,
			EType = EntityType.Ally,
		};
		await db.SaveEntitysAsync(entity);
		Entities.Add(entity);
	}

	[RelayCommand]
	async Task SaveEntityAsync(Entity entity)
	{
		if (CurrentEncounter == null) { return; }

		Entity newEntity = entity.Clone();
		newEntity.EncounterID = Constants.SavedEncounterID;

		await db.SaveEntitysAsync(newEntity);
	}

	[RelayCommand]
	async Task CopyEntityAsync(Entity entity)
	{
		if (CurrentEncounter == null) { return; }

		Entity newEntity = entity.Clone();

		await db.SaveEntitysAsync(newEntity);
		Entities.Add(newEntity);
	}
	public async Task GetEntities()
	{
		if (CurrentEncounter == null) { return; }
		if (IsBusy) return;

		try
		{
			IsBusy = true;
			var entityTemp = await db.GetEncounterEnitities(CurrentEncounter.ID);
			Debug.WriteLine($"SESSION ID is {entityTemp.Count}");
			if (Entities.Count != 0)
				Entities.Clear();

			foreach (var entity in entityTemp)
			{
				Entities.Add(entity);
			}
			SortEntities();
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("error", $"Encounter error{ex.Message}", "ok");
		}
		finally { IsBusy = false; }
	}

	[RelayCommand]
	public async Task DeleteEntityAsync(Entity entity)
	{
		if (CurrentEncounter == null) { return; }
		if (IsBusy) return;

		try
		{
			IsBusy = true;
			_ = await db.DeleteEntityAsync(entity);
			Entities.Remove(entity);
		}
		catch (Exception ex)
		{ Debug.WriteLine(ex); }
		finally
		{ IsBusy = false; }
	}

	public async Task SaveEncounter()
	{
		try
		{
			await db.SaveEncounterAsync(CurrentEncounter);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("error", $"Encounter error{ex.Message}", "ok");
		}

	}
	[RelayCommand]
	public async Task ShowInfoAsync(Entity entity)
	{
		await popupService.ShowPopupAsync<ExtraInfoViewModel>(onPresenting:
			ViewModel =>
			{
				ViewModel.CurrentEntity = entity;
				ViewModel.UpdatePostPopulate();
			});
		GetEntities();
	}

	[RelayCommand]
	public void SortEntities()
	{
		var orderedEntities = Entities.OrderByDescending(o => o.Initiative).ToList();
		Entities.Clear();
		foreach (var entity in orderedEntities)
			Entities.Add(entity);
	}

	[RelayCommand]
	public void RollInit(Entity entity)
	{
		var rand = new Random();
		int roll = rand.Next(1, 20);
		entity.Initiative = roll + entity.Perception;
		Debug.WriteLine($"{entity.Name}'s init is {entity.Initiative}");
	}

	[RelayCommand]
	public void RollAll()
	{
		foreach (Entity entity in Entities)
		{
			RollInit(entity);
		}
		SortEntities();
	}

	[RelayCommand]
	public async Task SaveEntitiesAsync()
	{
		foreach (Entity entity in Entities)
			await db.SaveEntitysAsync(entity);
	}

	[RelayCommand]
	public async Task GoToSelectEntityAsync()
	{
		Debug.WriteLine($"current encounter id is {CurrentEncounter.ID}");
		await Shell.Current.GoToAsync($"{nameof(SelectPremadeEntity)}?encounterid={CurrentEncounter.ID}");
	}

	[RelayCommand]
	public async Task GoToSavedEntityAsync()
	{
		Debug.WriteLine($"current encounter id is {CurrentEncounter.ID}");
		await Shell.Current.GoToAsync($"{nameof(SelectSavedEntity)}?encounterid={CurrentEncounter.ID}");
	}

	Entity lastDraggedOver;

	[RelayCommand]
	public void DragOver(Entity item)
	{
		lastDraggedOver = item;
		//Debug.WriteLine($"current drag over{lastDraggedOver.Name}");
	}

	[RelayCommand]
	public void Drop(Entity item)
	{
		try
		{
			if (lastDraggedOver != null)
			{
				item.Initiative = lastDraggedOver.Initiative;
				int currentItemPos = Entities.IndexOf(item);
				int newPos = Entities.IndexOf(lastDraggedOver);
				Entities.Move(currentItemPos, newPos);
			}
		}
		catch (Exception e)
		{
			Debug.WriteLine(e.Message);
		}
		finally
		{
			lastDraggedOver = null;
		}

		//CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		//string text = "This is a Toast";
		//ToastDuration duration = ToastDuration.Short;
		//double fontSize = 14;

		//var toast = Toast.Make(text, duration, fontSize);

		//await toast.Show(cancellationTokenSource.Token);
		//Debug.WriteLine($"position {lastDraggedOver.Name}");
	}
}
