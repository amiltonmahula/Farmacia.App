using System.Net.Http.Json;
using Farmacia.App.DTO.Request;
using Farmacia.App.DTO.Response;

namespace Farmacia.App.Service;

public class VendaService
{
    private readonly HttpClient _client;

    public VendaService(HttpClient client)
    {
        _client = client;
    }

    public async Task<VendaResponse?> CriarVendaAsync(VendaRequest request)
    {
        var resposta = await _client
            .PostAsJsonAsync("api/venda/CriarVenda", request);
        resposta.EnsureSuccessStatusCode();

        return await resposta.Content.ReadFromJsonAsync<VendaResponse>();
    }

    public async Task<List<VendaResponse>> ObterVendaAsync()
    {
        var vendas = await _client.GetFromJsonAsync<List<VendaResponse>>("api/venda");
        
        return vendas ?? new List<VendaResponse>();
    }
    public async Task<VendaResponse?> ObterVendaIdAsync(Guid id)
    {
        try
        {
            var detalhe = await _client.GetAsync($"api/venda/{id}");
            if (!detalhe.IsSuccessStatusCode)
                return null;
            return await detalhe.Content.ReadFromJsonAsync<VendaResponse>();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
            return null;
        }
        
    }
}