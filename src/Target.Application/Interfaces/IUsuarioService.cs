using Target.Application.Dtos;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> AdicionarUsuario(UsuarioDto model);
        Task<Usuarios> AtualizarUsuario(int usuarioId, UsuarioDto model);
        Task<bool> DeletarUsuario(int usuarioId);
        Task<List<Usuarios>> ObterListaUsuariosAsync();
        Task<Usuarios> ObterUsuarioPorIdAsync(int id);
        Task<bool> UsuarioExiste(string email);
    }
}