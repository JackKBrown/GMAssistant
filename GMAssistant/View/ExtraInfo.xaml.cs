using CommunityToolkit.Maui.Views;
using GMAssistant.ViewModel;
using System.Diagnostics;

namespace GMAssistant.View;

public partial class ExtraInfo : Popup
{
	readonly ExtraInfoViewModel viewModel;
	public ExtraInfo(ExtraInfoViewModel extraViewModel)
	{
		InitializeComponent();
		BindingContext = extraViewModel;
		viewModel = extraViewModel;
		viewModel.Finished += OnSelectLoaded;
		SetPopupSize();
	}

	private void SetPopupSize()
	{
		// Get the current window size
		var window = Application.Current?.MainPage?.Window;
		//var abilitycv = Application.Current?.MainPage?.

		if (window != null)
		{
			var windowWidth = window.Width;
			var windowHeight = window.Height;

			// Calculate 80% of the window's width and height
			var popupWidth = windowWidth * 0.8;
			var popupHeight = windowHeight * 0.8;

			// Set the size of the container (Grid) inside the popup
			PopupContainer.WidthRequest = popupWidth;
			PopupContainer.HeightRequest = popupHeight;

			AbilityCV.HeightRequest = 0.7 * popupHeight;
		}
	}

	void OnSelectLoaded(object? sender, EventArgs e)
	{
		this.Close();
	}

	protected override async Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
	{
		await viewModel.SaveChangesAsync();
		await base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
	}

	private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		Debug.WriteLine(e.Value);
		if (e.Value)
		{
			var radioButton = sender as RadioButton;

			string selectedValue = radioButton?.Value?.ToString();

			Debug.WriteLine($"selected value {selectedValue}");

			string selectedContent = radioButton?.Content?.ToString();

			Debug.WriteLine($"selected Content {selectedContent}");

			viewModel.CurrentEntity.EType = (Model.EntityType)Enum.Parse(typeof(Model.EntityType), selectedContent);
		}
	}
}


