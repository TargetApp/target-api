using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IcmPortal.Core.Dominio.Interfaces
{
    public interface IUsuarioLogado
    {
        string Nome { get; }
        int ObterUsuarioId();
        string ObterUsuarioEmail();
        bool EstaAutenticado();
        bool TemARole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
