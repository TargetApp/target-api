using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class ImagemPersist : IImagemPersist
    {
        private readonly StorageDbContext _context;

        public ImagemPersist(StorageDbContext context)
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