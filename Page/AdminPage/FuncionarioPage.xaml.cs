using Farmacia.App.ViewModel;

namespace Farmacia.App.Page.AdminPage;

public partial class FuncionarioPage : ContentPage
{
    private readonly UsuarioViewModel _usuarioViewModel;
    
    public FuncionarioPage(UsuarioViewModel usuarioViewModel)
    {
        InitializeComponent();
        _usuarioViewModel = usuarioViewModel;
        BindingContext = _usuarioViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((UsuarioViewModel)BindingContext).CarregarFuncionarios();
    }

    public async Task Inicializar()
    {
        await _usuarioViewModel.CarregarFuncionarios();
    }
}