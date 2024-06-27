using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IImagemPersist
    {
        Task<List<Imagens>> ObterListaImagensAsync();
        Task<Imagens> ObterImagemPorIdAsync(int id);
    }
}