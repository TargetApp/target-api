using System;
using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface ITecnicoService
    {
        Task<List<TecnicoDto>> ObterListaTecnicosAsync(string filter);
        Task<Tecnico> ObterTecnicoAsyncById(int id);
        Task<Tecnico> AdicionarTecnicoAsync(TecnicoDto model, int usuarioId);
        Task<Tecnico> AtualizarTecnicoAsync(int id, TecnicoAtualizarDto model, bool isEvaluation = false);
        Task<bool> ExcluirTecnicoAsync(int id);
    }
}
