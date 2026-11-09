using Farmacia.App.Page;

namespace Farmacia.App;

public partial class AppShell : Shell
{
    public AppShell(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(VendaDetalhePage),
            typeof(VendaDetalhePage));

        Items.Add(new ShellContent
        {
            Route = "LoginPage",
            Content = serviceProvider.GetRequiredService<LoginPage>()
        });
        Items.Add(new ShellContent
        {
            Route = "MainPage",
            Content = serviceProvider.GetRequiredService<MainPage>()
        });
    }
}