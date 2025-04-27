using Target.Domain.Dtos;
using Target.Application.Helpers;
using Target.Domain.Models;

namespace Target.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuarios> AdicionarUsuario(UsuarioCadastroDto model);
        Task<Usuarios> AtualizarUsuario(int usuarioId, UsuarioUpdateDto model);
        Task<Usuarios> AtualizarTokenLogin(Usuarios usuario);
        Task IncrementarTokenTentativas(int usuarioId, Usuarios usuario);
        Task<TokenValidations> VerificaTokenLogin(Usuarios usuario, string tokenLogin);
        Task<bool> DeletarUsuario(int usuarioId);
        Task<Usuarios> ObterUsuarioParametro(string param);
        Task<Usuarios> ObterUsuarioCadastradoAsync(UsuarioCadastroDto model);
        Task<Usuarios> ObterUsuarioPorIdAsync(int usuarioId);
        Task LimparTokenTentativas(Usuarios usuario);
    }
}