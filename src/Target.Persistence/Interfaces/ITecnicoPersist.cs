using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface ITecnicoPersist
    {
        Task<List<Tecnico>> ObterListaTecnicosAsync();
        Task<Tecnico> ObterTecnicoPorIdAsync(int id);
    }
}