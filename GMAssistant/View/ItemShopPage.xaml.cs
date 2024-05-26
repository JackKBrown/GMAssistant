using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class ItemShopPage : ContentPage
{
	ItemShopViewModel viewModel;
	public ItemShopPage(ItemShopViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		viewModel = vm;
	}
}