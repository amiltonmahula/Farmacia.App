namespace Farmacia.App.Models;

public class Venda
{
    public decimal Total { get; set; }
    
    // Produtos que fazem parte desta venda
    public ICollection<ItensVenda> Itens { get; set; } = new List<ItensVenda>();
}