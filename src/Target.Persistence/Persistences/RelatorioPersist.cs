using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class RelatorioPersist : IRelatorioPersist
    {
        private readonly TargetDbContext _context;

        public RelatorioPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<Relatorio> ObterRelatorioPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Relatorio>> ObterRelatoriosAsync()
        {
            throw new NotImplementedException();
        }

    }

}