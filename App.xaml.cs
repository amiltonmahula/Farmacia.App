using Farmacia.App.Page;
using Farmacia.App.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Farmacia.App;

public partial class App : Application
{
	private readonly VendaViewModel _viewModel;

	public App(VendaViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new VendaPage(_viewModel));
	}
}