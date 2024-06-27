using Microsoft.EntityFrameworkCore;
using Target.Domain.Dtos;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class TecnicoPersist : ITecnicoPersist
    {
        private readonly TargetDbContext _context;

        public TecnicoPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<List<TecnicoDto>> ObterListaTecnicosAsync()
        {
            var query = (
                from tecnico in _context.Tecnico
                join usuario in _context.Usuarios on tecnico.UserId equals usuario.Id
                select new TecnicoDto
                {
                    Id = tecnico.Id,
                    Name = usuario.Name,
                    Email = usuario.Email,
                    Telephone = usuario.Telephone,
                    ProfessionalQualification = tecnico.ProfessionalQualification,
                    OccupationArea = tecnico.OccupationArea,
                    CouncilRegistration = tecnico.CouncilRegistration,
                    Description = tecnico.Description,
                    Evaluation = tecnico.Evaluation,
                    IsExpanded = false
                }
            ).AsNoTracking();
            
            return await query.ToListAsync();
        }

        public async Task<Tecnico> ObterTecnicoPorIdAsync(int id)
        {
            var query = (
                from tecnico in _context.Tecnico
                where tecnico.Id == id
                select tecnico
            ).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}