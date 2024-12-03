using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GMAssistant.Model;

public class BoolProperty : INotifyPropertyChanged
{
	public string Name { get; set; }

	private bool _selected;
	public bool Selected
	{
		get => _selected;
		set
		{
			if (_selected != value)
			{
				_selected = value;
				OnPropertyChanged();  // Notify UI about the change
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
