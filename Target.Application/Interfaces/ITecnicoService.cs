using System;
using Target.Domain.Dtos;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface ITecnicoService
    {
        Task<List<TecnicoDto>> ObterListaTecnicosAsync();
        Task<Tecnico> ObterTecnicoAsyncById(int id);
        Task<Tecnico> AdicionarTecnicoAsync(TecnicoDto model, int usuarioId);
        Task<Tecnico> AtualizarTecnicoAsync(int id, TecnicoDto model);
        Task<bool> ExcluirTecnicoAsync(int id);
    }
}
