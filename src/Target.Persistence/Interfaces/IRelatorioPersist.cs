using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IRelatorioPersist
    {
        Task<List<Relatorio>> ObterRelatoriosAsync();
        Task<Relatorio> ObterRelatorioPorIdAsync(int id);
    }
}