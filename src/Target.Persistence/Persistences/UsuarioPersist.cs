using Microsoft.EntityFrameworkCore;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences
{
    public class UsuarioPersist : IUsuarioPersist
    {
        private readonly TargetDbContext _context;

        public UsuarioPersist(TargetDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuarios>> ObterListaUsuariosAsync()
        {
            var query = (
                from u in _context.Usuarios
                select u
            );

            return await query.ToListAsync();
        }

        public async Task<Usuarios> ObterUsuarioPorIdAsync(int id)
        {
            var query = (
                from u in _context.Usuarios
                where u.Id == id
                select u
            );

            return await query.FirstOrDefaultAsync();
        }

    }

}