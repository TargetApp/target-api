using AutoMapper;
using Target.Application.Dtos;
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
        private readonly IMapper _mapper;
        public UsuarioService(IUsuarioPersist usuarioPersist, IGeralPersist geralPersist, IMapper mapper)
        {
            _mapper = mapper;
            _geralPersist = geralPersist;
            _usuarioPersist = usuarioPersist;
        }

        public async Task<Usuarios> AdicionarUsuario(UsuarioCadastroDto model)
        {
            try
            {
                var param = model.Email != "" ? model.Email : model.Telefone != "" ? model.Telefone : null;
                if(param == null) throw new Exception("Email ou telefone devem ser informados");
            
                if(await UsuarioExiste(param))
                    throw new Exception("Usuário já cadastrado");

                var user = new Usuarios
                {
                    Email = model.Email,
                    Telefone = model.Telefone,
                    TokenTentativas = 0,
                    DataAtualizacaoToken = DateTime.Now,
                    DataCriacaoUsuario = DateTime.Now,
                    DataAtualizacaoUsuario = DateTime.Now
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
                throw new Exception($"Erro ao cadastrar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<Usuarios> AtualizarUsuario(int usuarioId, UsuarioUpdateDto model)
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
                usuario.DataAtualizacaoUsuario = DateTime.Now;
                
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

        public async void IncrementarTokenTentativas(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(usuarioId);
                usuario.TokenTentativas++;

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
                if(usuario is null) return null;

                if(!VerificaUsuarioBloqueado(usuario.TokenTentativas.Value, usuario.DataAtualizacaoToken.Value))
                {
                    var random = new Random();
                    var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var resultado = new string(Enumerable.Repeat(caracteres, 5)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());

                    usuario.TokenLogin = resultado;
                    usuario.DataAtualizacaoToken = DateTime.Now;
                    usuario.TokenTentativas = 0;

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

        public Task<TokenValidations> VerificaTokenLogin(UsuarioDto usuario, string tokenLogin)
        {
            if(usuario.DataAtualizacaoToken.AddMinutes(1) < DateTime.Now) return Task.FromResult(TokenValidations.TokenExpirado);

            else if(VerificaUsuarioBloqueado(usuario.TokenTentativas, usuario.DataAtualizacaoToken)) return Task.FromResult(TokenValidations.UsuarioBloqueado);
                
            else if(usuario.TokenLogin == tokenLogin) return Task.FromResult(TokenValidations.TokenValido);

            else return Task.FromResult(TokenValidations.TokenInvalido);
        }

        public static bool VerificaUsuarioBloqueado(int numTentativas, DateTime dataAtualizacaoToken)
        {
            if(numTentativas >= 3){
                if(DateTime.Now.AddMinutes(5) > dataAtualizacaoToken) return false; // Usuario não bloqueado
                return true; // Usuario bloqueado
            }
            return false; // Usuario não bloqueado
        }

        public async Task<UsuarioDto> ObterUsuarioPorIdAsync(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersist.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null) return null;

                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

                return usuarioDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter usuario cadastrado. Erro: {ex.Message}");
            }
        }
    }
}