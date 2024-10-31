using CommunityToolkit.Maui.Views;
using GMAssistant.ViewModel;

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
}


