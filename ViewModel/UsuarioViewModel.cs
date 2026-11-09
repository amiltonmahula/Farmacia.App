using System.Collections.ObjectModel;
using Farmacia.App.DTO.Request;
using Farmacia.App.DTO.Response;
using Farmacia.App.Service;

namespace Farmacia.App.ViewModel;

public sealed class UsuarioViewModel : BaseViewModel
{
    private readonly UsuarioService _usuarioService;

    public ObservableCollection<UsuarioResponse> Usuarios { get; set; } = new();
    
    private string _mensagem;

    public UsuarioViewModel(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    
    public string Pesquisa
    {
        get => _pesquisa; 
        
        set 
        { 
            if (SetField(ref _pesquisa, value))
                FiltrarFuncionarios();
        }
    }

    public string Mensagem
    {
        get => _mensagem; 
        set => SetField(ref _mensagem, value);
    }
    
    private bool _estaCarregando;

    public bool EstaCarregando
    {
        get => _estaCarregando; 
        set => SetField(ref _estaCarregando, value);
    }

    private List<UsuarioResponse> _todosUsuarios;
    private string _pesquisa;

    public async Task CarregarFuncionarios()
    {
        try
        {
            EstaCarregando = true;
            Mensagem = String.Empty;

            var resultado = await _usuarioService.ObterUsuarios();


            _todosUsuarios = resultado.ToList();
        }
        catch (Exception ex)
        {
            Mensagem = $"Erro ao carrregar Funcionários {ex.Message}";
        }
        finally
        {
            EstaCarregando = false;
        }
    }
    
    private void FiltrarFuncionarios()
    {
        Usuarios.Clear();
        
        var termo = Pesquisa.Trim();

        var filtradas = _todosUsuarios
            .Where(p => string.IsNullOrWhiteSpace(termo) || p.Nome.Contains(termo,
                                                             StringComparison.OrdinalIgnoreCase)
                                                         ||
                                                         p.Email.Contains(termo,
                                                             StringComparison.OrdinalIgnoreCase)).ToList();

        foreach (var funcionario in filtradas)
            Usuarios.Add(funcionario);
    }

    public async Task AdicionarFuncionario(string email, string nome, string senha)
    {
        var novoUsuario = new UsuarioRequest(nome, email, senha);
        var funcionario = _usuarioService.AdicionarAsync(novoUsuario);
    }
    
}