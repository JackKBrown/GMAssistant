using CommunityToolkit.Maui.Views;
using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class FilterPopup : Popup
{
	readonly FilterPopupViewModel viewModel;
	public FilterPopup(FilterPopupViewModel filterViewModel)
	{
		InitializeComponent();
		BindingContext = filterViewModel;
		viewModel = filterViewModel;
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


