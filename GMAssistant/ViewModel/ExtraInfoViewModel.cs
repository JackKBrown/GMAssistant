using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GMAssistant.Model;
using GMAssistant.Services;
using System.Diagnostics;

namespace GMAssistant.ViewModel;

public class EntInt
{
	public string Name { get; set; }
	public int Value { get; set; }
	public bool IsChecked { get; set; }
}

public partial class ExtraInfoViewModel : BaseViewModel
{


	GMADatabase database;

	[ObservableProperty]
	public Entity currentEntity;

	[ObservableProperty]
	public List<EntInt> entityOptions;

	[ObservableProperty]
	public int selectedValue;

	readonly WeakEventManager finishedEventManager = new();

	public ExtraInfoViewModel(GMADatabase dataBase)
	{
		Title = "All Encounters Page";
		database = dataBase;
		EntityOptions = new List<EntInt>();
		foreach (EntityType option in Enum.GetValues(typeof(EntityType)))
		{
			EntityOptions.Add(new EntInt
			{
				Name = option.ToString(),
				Value = (int)option,
				IsChecked = false
			});
		}
	}

	public void UpdatePostPopulate()
	{
		Debug.Write(CurrentEntity.Name);
		Debug.Write((int)CurrentEntity.EType);
		EntInt selected = EntityOptions.Where(i => i.Value == (int)CurrentEntity.EType).FirstOrDefault();
		selected.IsChecked = true;
	}

	public event EventHandler<EventArgs> Finished
	{
		add => finishedEventManager.AddEventHandler(value);
		remove => finishedEventManager.RemoveEventHandler(value);
	}

	[RelayCommand]
	public async Task SaveChangesAsync()
	{
		await database.SaveEntitysAsync(currentEntity);
	}

	[RelayCommand]
	public async Task DeleteEntityAsync()
	{
		await database.DeleteEntityAsync(currentEntity);
		finishedEventManager.HandleEvent(this, EventArgs.Empty, nameof(Finished));
	}
	[RelayCommand]
	public void RollInit()
	{
		Random rnd = new Random();
		currentEntity.Initiative = rnd.Next(1, 21) + currentEntity.Perception;
	}

}
