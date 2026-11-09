using Farmacia.App.Enum;

namespace Farmacia.App.DTO.Response;

public record UsuarioResponse(
    Guid Id,
    string Email,
    string Nome,
    bool Ativo,
    ECargo Cargo,
    DateTime CriadoEm);