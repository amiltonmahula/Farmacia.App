namespace Farmacia.App.DTO.Request;

public record VendaRequest(
    ICollection<ItensVendaRequest> Itens);   
