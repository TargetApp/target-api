using AutoMapper;
using Target.Domain.Dtos;
using Target.Application.Helpers;
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

        public async Task<Usuarios> AdicionarUsuario(UsuarioCadastroDto model)
        {
            try
            {
                var param = model.Email != "" ? model.Email : model.Telephone != "" ? model.Telephone : null;
                if(param == null) throw new Exception("Email ou telefone devem ser informados");
            
                if(await UsuarioExiste(param)) throw new Exception("Usuário já cadastrado");

                var user = new Usuarios
                {
                    Name = "",
                    AccountTypeId = 1,
                    RegisterTypeId = 1,
                    Email = model.Email,
                    Telephone = model.Telephone,
                    TokenAttempts = 0,
                    TokenUpdatedAt = DateTime.Now,
                };

                _geralPersist.Add(user);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<Usuarios> AtualizarUsuario(int usuarioId, UsuarioUpdateDto model)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null) return null;

                usuario.Email = model.Email;
                usuario.Name = model.Name;
                usuario.Telephone = model.Telephone;
                usuario.RegisterTypeId = model.RegisterTypeId;
                usuario.AccountTypeId = model.AccountTypeId;
                //usuario.Endereco = model.Endereco;
                usuario.UpdatedAt = DateTime.Now;
                
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
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async void IncrementarTokenTentativas(int usuarioId, Usuarios usuario)
        {
            try
            {
                usuario.TokenAttempts++;

                _geralPersist.Update(usuario);
                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incrementar token de tentativas. Erro: {ex.Message}");
            }

        }

        public async Task<Usuarios> AtualizarTokenLogin(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(usuarioId);
                if(usuario == null) return null;

                if(!VerificaUsuarioBloqueado(usuario.TokenAttempts.Value, usuario.TokenUpdatedAt.Value))
                {
                    var random = new Random();
                    var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var resultado = new string(Enumerable.Repeat(caracteres, 5)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());

                    usuario.TokenLogin = resultado;
                    usuario.TokenUpdatedAt = DateTime.Now;
                    usuario.TokenAttempts = 0;

                    _geralPersist.Update(usuario);
                }

                if (await _geralPersist.SaveChangesAsync())
                {
                    return usuario;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar token de login. Erro: {ex.Message}");
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

        public async Task<Usuarios> ObterUsuarioParametro(string param)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioParametroAsync(param);
                if (usuario == null) return null;

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter usuarios cadastrados. Erro: {ex.Message}");
            }
        }

        public async Task<Usuarios> ObterUsuarioCadastradoAsync(string email, string telefone)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioCadastradoAsync(email, telefone);
                if (usuario == null) return null;

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter usuario cadastrado. Erro: {ex.Message}");
            }
        }

        public Task<bool> UsuarioExiste(string param)
        {
            var usuario = _usuarioPersist.ObterUsuarioParametroAsync(param);
            if (usuario.Result == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public async Task<TokenValidations> VerificaTokenLogin(Usuarios usuario, string tokenLogin)
        {
            if(usuario.TokenUpdatedAt.Value.AddMinutes(1) < DateTime.Now) return TokenValidations.TokenExpirado;

            else if(VerificaUsuarioBloqueado(usuario.TokenAttempts.Value, usuario.TokenUpdatedAt.Value)) return TokenValidations.UsuarioBloqueado;
                
            else if(usuario.TokenLogin == tokenLogin) return TokenValidations.TokenValido;

            else return TokenValidations.TokenInvalido;
        }

        public static bool VerificaUsuarioBloqueado(int numTentativas, DateTime dataAtualizacaoToken)
        {
            if(numTentativas >= 3){
                if(DateTime.Now.AddMinutes(5) > dataAtualizacaoToken) return false; // Usuario não bloqueado
                return true; // Usuario bloqueado
            }
            return false; // Usuario não bloqueado
        }

        public async Task<Usuarios> ObterUsuarioPorIdAsync(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null) return null;

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter usuario cadastrado. Erro: {ex.Message}");
            }
        }
    }
}