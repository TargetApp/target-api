using System;
using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId);
        Task<Relatorio> AdicionarRelatorioAsync(RelatorioCriarDto relatorioDto, int usuarioId);
        Task<Relatorio> AtualizarRelatorioAsync(int relatorioId, RelatorioDto relatorioDto);
        Task<bool> ExcluirRelatorioAsync(int relatorioId);
    }
}
