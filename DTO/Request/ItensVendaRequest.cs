namespace Farmacia.App.DTO.Request;

public record ItensVendaRequest(
    Guid ProdutoId,
    int Quantidade);