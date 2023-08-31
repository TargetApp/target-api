using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuarios> AdicionarUsuario(Usuarios model);
        Task<Usuarios> AtualizarUsuario(int usuarioId, Usuarios model);
        Task<bool> DeletarUsuario(int usuarioId);
        Task<List<Usuarios>> ObterListaUsuariosAsync();
        Task<Usuarios> ObterUsuarioPorIdAsync(int id);
    }
}