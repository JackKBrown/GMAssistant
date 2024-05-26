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
			builder.Services.AddSingleton<SelectPremadeEntity>();
			builder.Services.AddSingleton<ItemShopPage>();

			builder.Services.AddSingleton<AllSessionsViewModel>();
			builder.Services.AddSingleton<AllEncountersViewModel>();
			builder.Services.AddSingleton<SelectPremadeEntityViewModel>();
			builder.Services.AddSingleton<ItemShopViewModel>();

			builder.Services.AddSingleton<GMADatabase>();
			builder.Services.AddSingleton<BestiaryService>();
			
			builder.Services.AddTransient<SessionViewModel>();
			builder.Services.AddTransient<EncounterViewModel>();
			builder.Services.AddTransient<SessionPage>();
			builder.Services.AddTransient<EncounterPage>();
			

			builder.Services.AddTransientPopup<ExtraInfo, ExtraInfoViewModel>();
			builder.Services.AddTransientPopup<FilterPopup, FilterPopupViewModel>();

			return builder.Build();
		}
	}
}
