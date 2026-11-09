using System.Windows.Input;
using Farmacia.App.DTO.Request;
using Farmacia.App.Service;

namespace Farmacia.App.ViewModel;

public class LoginViewModel : BaseViewModel
{
    private readonly UsuarioService _service;
    private string _mensagem;
    private string _email;
    private string _senha;
    private bool _estaCarregando;
    
    public string Mensagem
    {
        get => _mensagem; 
        set => SetField(ref _mensagem, value);
    }

    public bool EstaCarregando
    {
        get => _estaCarregando; 
        set => SetField(ref _estaCarregando, value);
    }

    public string Email
    {
        get => _email;
        set => SetField(ref _email, value);
    }

    public string Senha
    {
        get => _senha; 
        set => SetField(ref _senha, value);
    }
    
    public ICommand LoginCommand { get; }

    public LoginViewModel(UsuarioViewModel usuarioViewModel, UsuarioService service)
    {
        _service = service;
        LoginCommand = new Command(async () => await LoginAsync());
    }

    private async Task LoginAsync()
    {
        try
        {
            EstaCarregando = true;
            Mensagem = string.Empty;

            var request = new LoginRequest
            {
                Email = Email,
                Senha = Senha

            };

            var resultado = await _service.LoginAsync(request);
            if (!string.IsNullOrWhiteSpace(resultado.Token))
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
            
            //Mensagem = $"Bem-vindo, {resultado.Nome}!";

        }
        catch (Exception ex)
        {
            Mensagem = ex.ToString();
        }
        finally
        {
            EstaCarregando = false;
        }
    }
}