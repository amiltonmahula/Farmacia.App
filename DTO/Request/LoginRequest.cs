using System.ComponentModel.DataAnnotations;

namespace Farmacia.App.DTO.Request;

public record LoginRequest
{
    [EmailAddress(ErrorMessage = "Email obrigatório")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Senha obrigatória")]
    public string Senha { get; set; }
}
    