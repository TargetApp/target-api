using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IImagemPersist
    {
        void SetImagemBase64(List<RelatorioDto> relatorios);
        Task<List<Imagens>> ObterListaImagensAsync();
        Task<Imagens> ObterImagemPorIdAsync(int id);
    }
}