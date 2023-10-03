using Target.Application.Dtos;
using Target.Application.Helpers;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuarios> AdicionarUsuario(UsuarioCadastroDto model);
        Task<Usuarios> AtualizarUsuario(int usuarioId, UsuarioUpdateDto model);
        Task<Usuarios> AtualizarTokenLogin(int usuarioId);
        void IncrementarTokenTentativas(int usuarioId);
        Task<TokenValidations> VerificaTokenLogin(UsuarioDto usuario, string tokenLogin);
        Task<bool> DeletarUsuario(int usuarioId);
        Task<Usuarios> ObterUsuarioParametro(string param);
        Task<Usuarios> ObterUsuarioCadastradoAsync(string email, string telefone);
        Task<UsuarioDto> ObterUsuarioPorIdAsync(int usuarioId);
        Task<bool> UsuarioExiste(string param);
    }
}