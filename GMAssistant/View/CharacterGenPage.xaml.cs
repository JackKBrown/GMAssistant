using GMAssistant.ViewModel;

namespace GMAssistant.View;

public partial class CharacterGenPage : ContentPage
{
	CharacterGenViewModel viewModel;
	public CharacterGenPage(CharacterGenViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		viewModel = vm;
	}
}