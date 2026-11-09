using Farmacia.App.Enum;

namespace Farmacia.App.DTO.Request;

public record UsuarioRequest(
    string Nome,
    string Email,
    string Senha);