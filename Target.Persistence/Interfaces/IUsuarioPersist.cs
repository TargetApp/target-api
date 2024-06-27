using Target.Domain.Models;

namespace Target.Persistence.Interfaces
{
    public interface IUsuarioPersist
    {
        Task<Usuarios> ObterUsuarioParametroAsync(string param);
        Task<Usuarios> ObterUsuarioPorIdAsync(int usuarioId);
        Task<Usuarios> ObterUsuarioCadastradoAsync(string email, string telefone);
    }
}