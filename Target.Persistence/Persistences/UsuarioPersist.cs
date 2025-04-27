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

        public async Task<Usuarios> ObterUsuarioParametroAsync(string param)
        {
            var query = (
                from u in _context.Usuarios
                where u.Email == param || u.Telephone == param
                select u
            );

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuarios> ObterUsuarioPorIdAsync(int usuarioId)
        {
            var query = (
                from u in _context.Usuarios
                where u.Id == usuarioId
                select u
            );

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuarios> ObterUsuarioCadastradoByEmailAsync(string email)
        {
            var query = (
                from u in _context.Usuarios
                where u.Email == email
                select u
            );

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuarios> ObterUsuarioCadastradoByTelephoneAsync(string telephone)
        {
            var query = (
                from u in _context.Usuarios
                where u.Telephone == telephone
                select u
            );

            return await query.FirstOrDefaultAsync();
        }
    }

}