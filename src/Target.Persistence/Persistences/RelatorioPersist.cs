using Microsoft.EntityFrameworkCore;
using Target.Domain.Dtos;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class RelatorioPersist : IRelatorioPersist
    {
        private readonly TargetDbContext _context;

        public RelatorioPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<Relatorio> ObterRelatorioPorIdAsync(int id)
        {
            var query = (
                from relatorio in _context.Relatorio
                where relatorio.Id == id
                select relatorio
            ).AsNoTracking();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<RelatorioDto>> ObterRelatoriosAsync()
        {
            var query = (
                from relatorio in _context.Relatorio
                select new RelatorioDto
                {
                    Nome = relatorio.Nome,
                    DataCriacao = relatorio.DataCriacao,
                }
            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<List<RelatorioDto>> ObterRelatoriosPorUsuarioIdAsync(int usuarioId)
        {
            var query = (
                from relatorio in _context.Relatorio
                where relatorio.UsuarioId == usuarioId
                select new RelatorioDto
                {
                    Nome = relatorio.Nome,
                    DataCriacao = relatorio.DataCriacao,
                }
            ).AsNoTracking();

            return await query.ToListAsync();
        }

    }

}