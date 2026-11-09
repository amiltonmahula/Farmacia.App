namespace Farmacia.App.Models;

public class ItensVenda
{
        public Guid VendaId { get; set; }
        public Venda Venda { get; set; } = null;
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        
        public decimal Subtotal => PrecoUnitario * Quantidade; 
}