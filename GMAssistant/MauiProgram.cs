using GMAssistant.Services;
using GMAssistant.View;
using GMAssistant.ViewModel;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace GMAssistant
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
				});
#if DEBUG
			builder.Logging.AddDebug();
#endif
			builder.Services.AddSingleton<AllSessions>();
			builder.Services.AddSingleton<AllEncounters>();
			builder.Services.AddSingleton<GMADatabase>();
			builder.Services.AddSingleton<BestiaryService>();
			builder.Services.AddSingleton<AllSessionsViewModel>();
			builder.Services.AddSingleton<AllEncountersViewModel>();

			builder.Services.AddTransient<SessionViewModel>();
			builder.Services.AddTransient<EncounterViewModel>();
			builder.Services.AddTransient<SelectPremadeEntityViewModel>();
			builder.Services.AddTransient<SessionPage>();
			builder.Services.AddTransient<EncounterPage>();
			builder.Services.AddTransient<SelectPremadeEntity>();

			builder.Services.AddTransientPopup<ExtraInfo, ExtraInfoViewModel>();

			return builder.Build();
		}
	}
}
