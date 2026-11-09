using Farmacia.App.DTO.Request;
using Farmacia.App.Models;

namespace Farmacia.App.DTO.Response;

public record VendaResponse(
    Guid Id, 
    DateTime CriadoEm,
    decimal Total,
    ICollection<ItensVendaResponse> Itens);