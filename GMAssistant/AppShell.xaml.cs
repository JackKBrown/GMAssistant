using GMAssistant.View;

namespace GMAssistant
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(SessionPage), typeof(SessionPage));
			Routing.RegisterRoute(nameof(EncounterPage), typeof(EncounterPage));
		}
	}
}
