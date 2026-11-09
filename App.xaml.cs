using Farmacia.App.Page;
using Farmacia.App.Page.AdminPage;
using Farmacia.App.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Farmacia.App;

public partial class App : Application
{
	private readonly AppShell _app;
	
	public App(AppShell app)
	{
		_app = app;

		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(_app);
	}
}