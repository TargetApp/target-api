using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IProdutorPersist
    {
        Task<List<Produtor>> ObterTodosProdutoresAsync();
        Task<Produtor> ObterProdutorPorIdAsync(int id);
    }
}