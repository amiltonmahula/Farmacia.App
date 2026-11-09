using Farmacia.App.Enum;

namespace Farmacia.App.DTO.Response;

public record LoginResponse(
    string Token,
    Guid UsuarioId,
    string Nome,
    string Email,
    ECargo Cargo
    );