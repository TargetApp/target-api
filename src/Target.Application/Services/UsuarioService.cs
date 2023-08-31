using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioPersist _usuarioPersist;
        public UsuarioService(IUsuarioPersist usuarioPersist)
        {
            _usuarioPersist = usuarioPersist;
        }

        public async Task<Usuarios> AdicionarUsuario(Usuarios model)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuarios> AtualizarUsuario(int usuarioId, Usuarios model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletarUsuario(int usuarioId)
        {
            throw new NotImplementedException();
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