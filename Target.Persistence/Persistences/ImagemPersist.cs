using Target.Domain.Dtos;
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

        public void SetImagemBase64(List<RelatorioDto> relatorios)
        {
            foreach(var relatorio in relatorios)
            {
                var imageBase64 = _context.ImageStorage.FirstOrDefault(x => x.Id == relatorio.ImageId)?.Data;
                if(imageBase64 != null)
                    relatorio.ImageBase64 = Convert.ToBase64String(imageBase64);
                else
                    relatorio.ImageBase64 = null;
            }
        }
    }
}