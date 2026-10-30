using Farmacia.App.Page;
using Farmacia.App.Service;
using Farmacia.App.ViewModel;
using Microsoft.Extensions.Logging;

namespace Farmacia.App;

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
		builder.Services.AddHttpClient<ProdutoService>(client =>
		{
			client.BaseAddress = new Uri("http://localhost:5106/");
		});
		
		builder.Services.AddHttpClient<VendaService>(client =>
		{
			client.BaseAddress = new Uri("http://localhost:5106/");
		});

		// Injenção das ViewModel
		builder.Services.AddTransient<ProdutosViewModel>();
		builder.Services.AddTransient<VendaViewModel>();
		// Injenção das View
		builder.Services.AddTransient<ProdutoPage>();
		builder.Services.AddTransient<VendaPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}
