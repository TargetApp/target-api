using Target.Domain.Dtos;
using Target.Domain.Models;
using Target.Domain.ViewModels;

namespace Target.Persistence.Interfaces
{
    public interface IRelatorioPersist
    {
        Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId);
        Task<RelatorioDto> ObterRelatorioPorIdAsync(int id);
        Task<List<RelatorioDto>> ObterRelatoriosPorDataAsync(int ano, int mes, int dia, int usuarioId);
    }
}