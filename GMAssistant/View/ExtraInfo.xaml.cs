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


