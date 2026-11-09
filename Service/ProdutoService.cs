using System.Net.Http.Json;
using Farmacia.App.DTO.Request;
using Farmacia.App.DTO.Response;
using Farmacia.App.Models;

namespace Farmacia.App.Service;

public class ProdutoService
{
    private readonly HttpClient _client;

    public ProdutoService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Produto>> ObterProdutoAsync()
    {
        var produtos = await _client.GetFromJsonAsync<List<Produto>>("api/produto");
        
        return produtos ?? new List<Produto>();
    }

    public async Task<ProdutoResponse?> AdicionarProdutoAsync(ProdutoRequest produto)
    {
        var resultado = await _client.PostAsJsonAsync("api/produto/CriarProduto", produto);

        resultado.EnsureSuccessStatusCode();

        return await resultado.Content.ReadFromJsonAsync<ProdutoResponse>();
    }
}