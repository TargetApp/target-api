using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class ImagemPersist : IImagemPersist
    {
        private readonly TargetDbContext _context;

        public ImagemPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<Imagens> ObterImagemPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Imagens>> ObterListaImagensAsync()
        {
            throw new NotImplementedException();
        }
    }
}