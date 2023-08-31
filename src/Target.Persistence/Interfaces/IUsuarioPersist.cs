using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IUsuarioPersist
    {
        Task<List<Usuarios>> ObterListaUsuariosAsync();
        Task<Usuarios> ObterUsuarioPorIdAsync(int id);
    }
}