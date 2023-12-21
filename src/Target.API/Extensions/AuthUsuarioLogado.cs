using IcmPortal.Core.Dominio.Interfaces;
using System.Security.Claims;

namespace Target.API.Extensions
{
    public class AuthUsuarioLogado : IUsuarioLogado
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthUsuarioLogado(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Nome => _accessor.HttpContext.User.Identity.Name;

        public int ObterUsuarioId()
        {
            return EstaAutenticado() ? int.Parse(_accessor.HttpContext.User.GetUsuarioId()) : 0;
        }

        public string ObterUsuarioEmail()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUsuarioEmail() : "";
        }

        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }


        public bool TemARole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsuarioId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUsuarioEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
