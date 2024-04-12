using GMAssistant.ViewModel;
using System.Security.Cryptography.X509Certificates;

namespace GMAssistant.View;

public partial class AllSessions : ContentPage
{
	AllSessionsViewModel ViewModel;
	public AllSessions(AllSessionsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		ViewModel = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		ViewModel.GetSessionsCommand.Execute(this);
	}
}