using Farmacia.App.Page;
using Farmacia.App.Page.AdminPage;
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
		builder.Services.AddTransient<AuthTokenHandler>();
		builder.Services.AddHttpClient<ProdutoService>(client =>
		{
			client.BaseAddress = new Uri("http://localhost:5106/");
		}).AddHttpMessageHandler<AuthTokenHandler>();
		
		builder.Services.AddHttpClient<VendaService>(client =>
		{
			client.BaseAddress = new Uri("http://localhost:5106/");
		}).AddHttpMessageHandler<AuthTokenHandler>();
		
		builder.Services.AddHttpClient<UsuarioService>(client =>
		{
			client.BaseAddress = new Uri("http://localhost:5106/");
		});

		// Injenção das ViewModel
		builder.Services.AddTransient<ProdutosViewModel>();
		builder.Services.AddTransient<VendaViewModel>();
		builder.Services.AddTransient<HistoricoViewModel>();
		builder.Services.AddTransient<DashBoardViewModel>();
		builder.Services.AddTransient<RelatorioViewModel>();
		builder.Services.AddTransient<UsuarioViewModel>();
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<VendaDetalheViewModel>();
		// Injenção das View
		builder.Services.AddTransient<ProdutoPage>();
		builder.Services.AddTransient<AppShell>();
		builder.Services.AddTransient<VendaPage>();
		builder.Services.AddTransient<HistoricoPage>();
		builder.Services.AddTransient<DashBoardPage>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<RegistrarPage>();
		builder.Services.AddTransient<RelatorioPage>();
		builder.Services.AddTransient<AdminMainPage>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<FuncionarioPage>();
		builder.Services.AddTransient<VendaDetalhePage>();
		builder.Services.AddTransient<EsqueciSenhaPage>();

		builder.Services.AddSingleton<App>();
		

#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}
