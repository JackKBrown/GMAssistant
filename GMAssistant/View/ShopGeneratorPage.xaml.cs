using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class ShopGeneratorPage : ContentPage
{
	ShopGeneratorViewModel ViewModel;
	public ShopGeneratorPage(ShopGeneratorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel;
		ViewModel = viewModel;
	}
}