using System;
using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId);
        Task<RelatorioClassificacao> AdicionarRelatorioAsync(RelatorioCriarDto relatorioDto, int usuarioId);
        Task<RelatorioClassificacao> AtualizarRelatorioAsync(int relatorioId, RelatorioDto relatorioDto);
        Task<bool> ExcluirRelatorioAsync(int relatorioId);
        Task<int> InsertClassificationReport(int userId, int imageId, int modelId);
    }
}
