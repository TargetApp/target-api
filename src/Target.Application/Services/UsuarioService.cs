using Target.Application.Dtos;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioPersist _usuarioPersist;
        private readonly IGeralPersist _geralPersist;
        public UsuarioService(IUsuarioPersist usuarioPersist, IGeralPersist geralPersist)
        {
            _geralPersist = geralPersist;
            _usuarioPersist = usuarioPersist;
        }

        public async Task<UsuarioDto> AdicionarUsuario(UsuarioDto model)
        {
            try
            {
                var user = new Usuarios
                {
                    Email = model.Email,
                    Nome = model.Nome,
                    Telefone = model.Telefone,
                    TipoCadastro = model.TipoCadastro,
                    TipoConta = model.TipoConta,
                    Endereco = model.Endereco
                };

                _geralPersist.Add(user);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<Usuarios> AtualizarUsuario(int usuarioId, UsuarioDto model)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null) return null;

                usuario.Email = model.Email;
                usuario.Nome = model.Nome;
                usuario.Telefone = model.Telefone;
                usuario.TipoCadastro = model.TipoCadastro;
                usuario.TipoConta = model.TipoConta;
                usuario.Endereco = model.Endereco;
                
                _geralPersist.Update(usuario);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var usuarioRetorno = await _usuarioPersist.ObterUsuarioPorIdAsync(usuario.Id);
                    return usuarioRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> DeletarUsuario(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null) throw new Exception("Usuário não encontrado.");

                _geralPersist.Delete(usuario);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<List<Usuarios>> ObterListaUsuariosAsync()
        {
            try
            {
                var usuarios = await _usuarioPersist.ObterListaUsuariosAsync();
                if (usuarios == null) return null;

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter usuarios cadastrados. Erro: {ex.Message}");
            }
        }

        public async Task<Usuarios> ObterUsuarioPorIdAsync(int id)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(id);
                if (usuario == null) return null;

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter usuario por id. Erro: {ex.Message}");
            }
        }

        public Task<bool> UsuarioExiste(string email)
        {
            var usuario = _usuarioPersist.ObterListaUsuariosAsync().Result.FirstOrDefault(x => x.Email == email);
            if (usuario == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}