using System;
using Target.Application.Dtos;

namespace Target.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJwtToken(UsuarioDto usuarioDto);
    }
}
