namespace Farmacia.App.Models;

public class Produto
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = String.Empty;
    
    public decimal Preco { get; set; }
    
    public int Quantidade { get; set; }
    
    public DateTime DataValidade { get; set; }
    
    public string Descricao { get; set; } = String.Empty;

    public string CodigoDeBarra { get; set; } = String.Empty;

    public DateTime CriadoEm { get; set; }
}