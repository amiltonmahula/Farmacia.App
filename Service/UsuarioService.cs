using System.Net.Http.Json;
using Farmacia.App.DTO.Request;
using Farmacia.App.DTO.Response;

namespace Farmacia.App.Service;

public class UsuarioService
{
    private readonly HttpClient _client;

    public UsuarioService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<UsuarioResponse>> ObterUsuarios()
    {
        var resultado = await _client.GetFromJsonAsync<List<UsuarioResponse>>("api/auth");

        return resultado ?? new List<UsuarioResponse>();
    }

    public async Task<UsuarioResponse?> AdicionarAsync(UsuarioRequest usuario)
    {
        var resultado = await _client.PostAsJsonAsync("api/auth/registar", usuario);

        resultado.EnsureSuccessStatusCode();
        
        return await resultado.Content.ReadFromJsonAsync<UsuarioResponse>();
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest login)
    {
        var response = await _client.PostAsJsonAsync("api/auth/login", login);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Email ou senha inválidos");

        var resultado = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (resultado is null || string.IsNullOrWhiteSpace(resultado.Token))
            throw new Exception("Não foi possivel obter o token");
        await SecureStorage.Default.SetAsync("auth_token", resultado.Token);

        return resultado;
    }
    
    public async Task LogOut()
    {
        SecureStorage.Default.Remove("auth_token");
    }
}