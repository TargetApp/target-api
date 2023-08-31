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
            throw new NotImplementedException();
        }

        public async Task<Usuarios> ObterUsuarioPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }

}