using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class RegistrarPage : ContentPage
{
    private readonly UsuarioViewModel _usuarioViewModel;
    
    public RegistrarPage(UsuarioViewModel usuarioViewModel)
    {
        InitializeComponent();
        _usuarioViewModel = usuarioViewModel;
        BindingContext = _usuarioViewModel;
    }
    

    private async void Registrar_OnClicked(object? sender, EventArgs e)
    {
        await _usuarioViewModel.AdicionarFuncionario(TxtEmail.Text, TxtNome.Text, TxtSenha.Text);

        await DisplayAlertAsync("Sucesso", "Criado com sucesso", "Ok", "Sair");
    }
}