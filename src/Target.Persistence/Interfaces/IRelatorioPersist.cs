using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IRelatorioPersist
    {
        Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId);
        Task<Relatorio> ObterRelatorioPorIdAsync(int id);
    }
}