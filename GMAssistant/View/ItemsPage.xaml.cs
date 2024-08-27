using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class ItemsPage : ContentPage
{
	ItemsViewModel viewModel;
	public ItemsPage(ItemsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		viewModel = vm;
	}
}