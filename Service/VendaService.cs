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

}