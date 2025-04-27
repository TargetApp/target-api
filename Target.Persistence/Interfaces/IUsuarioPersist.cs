using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IUsuarioPersist
    {
        Task<Usuarios> ObterUsuarioParametroAsync(string param);
        Task<Usuarios> ObterUsuarioPorIdAsync(int usuarioId);
        Task<Usuarios> ObterUsuarioCadastradoByEmailAsync(string email);
        Task<Usuarios> ObterUsuarioCadastradoByTelephoneAsync(string telephone);
    }
}