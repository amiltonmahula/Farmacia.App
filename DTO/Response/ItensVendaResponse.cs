namespace Farmacia.App.DTO.Response;

public record ItensVendaResponse(
    Guid ProdutoId,
    string NomeProduto,
    int Quantidade,
    decimal PrecoUnitario,
    decimal SubTotal);