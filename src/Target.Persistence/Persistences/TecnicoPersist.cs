using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class TecnicoPersist : ITecnicoPersist
    {
        private readonly TargetDbContext _context;

        public TecnicoPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tecnico>> ObterListaTecnicosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Tecnico> ObterTecnicoPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}