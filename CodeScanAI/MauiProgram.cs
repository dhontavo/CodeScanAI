using CodeScanAI.Services;
using CodeScanAI.ViewModels;
using CodeScanAI.Views;
using Microsoft.Extensions.Logging;

namespace CodeScanAI;

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
        builder.Services.AddSingleton<IAiService, AnthropicService>();
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<HistoryViewModel>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<ResultPage>();

        // Registrar ruta para navegacion
        Routing.RegisterRoute("result", typeof(ResultPage));

        return builder.Build();
	}
}
