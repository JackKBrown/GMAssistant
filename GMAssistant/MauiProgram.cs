using GMAssistant.Services;
using GMAssistant.View;
using GMAssistant.ViewModel;
using Microsoft.Extensions.Logging;

namespace GMAssistant
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});
#if DEBUG
			builder.Logging.AddDebug();
#endif
			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddSingleton<AllSessions>();
			builder.Services.AddSingleton<AllEncounters>();
			builder.Services.AddSingleton<GMADatabase>();
			builder.Services.AddSingleton<AllSessionsViewModel>();
			builder.Services.AddSingleton<AllEncountersViewModel>();

			builder.Services.AddTransient<SessionViewModel>();
			builder.Services.AddTransient<EncounterViewModel>();
			builder.Services.AddTransient<SessionPage>();
			builder.Services.AddTransient<EncounterPage>();

			return builder.Build();
		}
	}
}
