using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Handlers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using GMAssistant.View;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAssistant.ViewModel;

public partial class ExtraInfoViewModel : BaseViewModel
{
	GMADatabase database;

	[ObservableProperty]
	public Entity currrentEntity;

	readonly WeakEventManager finishedEventManager = new();

	public ExtraInfoViewModel(GMADatabase dataBase)
	{
		Title = "All Encounters Page";
		database = dataBase;
	}

	public event EventHandler<EventArgs> Finished
	{
		add => finishedEventManager.AddEventHandler(value);
		remove => finishedEventManager.RemoveEventHandler(value);
	}

	[RelayCommand]
	public async Task SaveChangesAsync()
	{
		await database.SaveEntitysAsync(CurrrentEntity);
	}

	[RelayCommand]
	public async Task DeleteEntityAsync()
	{
		await database.DeleteEntityAsync(CurrrentEntity);
		finishedEventManager.HandleEvent(this, EventArgs.Empty, nameof(Finished));
	}
	[RelayCommand]
	public void RollInit()
	{
		Random rnd = new Random();
		currrentEntity.Initiative = rnd.Next(1,21) + currrentEntity.Perception;
	}

}
