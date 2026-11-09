using System.ComponentModel.DataAnnotations;

namespace Farmacia.App.DTO.Request;

public record ProdutoRequest
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "Preço obrigatório")]
    public decimal Preco { get; set; }
    
    public int QuantidadeEstoque { get; set; }
    
    [Required(ErrorMessage = "Data de validade obrigatória")]
    public DateTime DataDeValidade { get; set; }

    public string? Descricao { get; set; }
    
    public string? CodigoDeBarra { get; set; }
}