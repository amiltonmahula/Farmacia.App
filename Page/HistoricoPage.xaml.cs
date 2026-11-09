using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class HistoricoPage : ContentPage
{
    private readonly HistoricoViewModel _historicoViewModel;
    public HistoricoPage(HistoricoViewModel historicoViewModel)
    {
        InitializeComponent();
        _historicoViewModel = historicoViewModel;
        BindingContext = _historicoViewModel;
    }

    public async Task InicializarAsync()
    {
        await _historicoViewModel.CarregarVendaAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InicializarAsync();
    }
}