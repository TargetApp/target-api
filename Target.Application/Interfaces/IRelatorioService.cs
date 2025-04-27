using System;
using Target.Domain.Dtos;
using Target.Domain.Models;
using Target.Domain.ViewModels;

namespace Target.Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId);
        Task<List<RelatorioDto>> ObterRelatoriosPorDataAsync(int ano, int mes, int dia, int usuarioId);
        Task<AnaliseMensalViewModel> ObterAnalisesPorMes(int usuarioId, int mes);
        Task<RelatorioDto> ObterRelatorioPorIdAsync(int relatorioId);
        Task<RelatorioClassificacao> AdicionarRelatorioAsync(RelatorioCriarDto relatorioDto, int usuarioId);
        Task<RelatorioDto> AtualizarRelatorioAsync(int relatorioId, RelatorioDto relatorioDto);
        Task<bool> ExcluirRelatorioAsync(int relatorioId);
        Task<int> InsertClassificationReport(int userId, int imageId, int modelId);
    }
}
