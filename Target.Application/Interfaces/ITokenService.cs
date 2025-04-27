using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJwtToken(Usuarios usuarioDto);
    }
}
