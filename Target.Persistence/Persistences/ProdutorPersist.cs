using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class ProdutorPersist : IProdutorPersist
    {
        private readonly TargetDbContext _context;

        public ProdutorPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<Produtor> ObterProdutorPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Produtor>> ObterTodosProdutoresAsync()
        {
            throw new NotImplementedException();
        }

    }

}