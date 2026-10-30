namespace Farmacia.App.DTO.Request;

public record ItensRequest(
    Guid ProdutoId,
    int Quantidade);