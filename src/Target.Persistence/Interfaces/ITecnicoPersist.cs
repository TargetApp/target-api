using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface ITecnicoPersist
    {
        Task<List<TecnicoDto>> ObterListaTecnicosAsync();
        Task<Tecnico> ObterTecnicoPorIdAsync(int id);
    }
}