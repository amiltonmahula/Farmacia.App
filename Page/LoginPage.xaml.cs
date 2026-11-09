using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;
    public LoginPage(LoginViewModel loginViewModel)
    {
        Email.Focus();
        _loginViewModel = loginViewModel;
        InitializeComponent();
        BindingContext = _loginViewModel;
    }

    private async void Tap_RecuperarSenha_OnTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("/EsqueciSenhaPage");
    }
    
    private void LimparCaixas()
    {
        
    }
}