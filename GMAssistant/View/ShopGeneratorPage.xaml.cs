using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class ShopGeneratorPage : ContentPage
{
	ShopGeneratorViewModel ViewModel;
	public ShopGeneratorPage(ShopGeneratorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		ViewModel = viewModel;
	}

	private void levelpicker_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
}