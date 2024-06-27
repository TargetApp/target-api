using System;
using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId);
        Task<RelatorioDto> ObterRelatorioPorIdAsync(int relatorioId);
        Task<RelatorioClassificacao> AdicionarRelatorioAsync(RelatorioCriarDto relatorioDto, int usuarioId);
        Task<RelatorioDto> AtualizarRelatorioAsync(int relatorioId, RelatorioDto relatorioDto);
        Task<bool> ExcluirRelatorioAsync(int relatorioId);
        Task<int> InsertClassificationReport(int userId, int imageId, int modelId);
    }
}
