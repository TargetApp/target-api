using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Domain.Dtos;
using Target.Application.Helpers;
using Target.Application.Interfaces;
namespace Target.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/conta")]
public class TargetAccountController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    public TargetAccountController(IUsuarioService usuarioService, ITokenService tokenService, IEmailService emailService, IConfiguration configuration)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
        _emailService = emailService;
        _configuration = configuration;
    }

    [HttpPost("pre-login")]
    [AllowAnonymous]
    public async Task<IActionResult> PreLogin(UsuarioCadastroDto model)
    {
        try
        {

            var usuario = await _usuarioService.ObterUsuarioCadastradoAsync(model);
            if (usuario == null) usuario = await _usuarioService.AdicionarUsuario(model);

            var usuarioToken = await _usuarioService.AtualizarTokenLogin(usuario);
            if (usuarioToken == null) return NotFound("Erro ao gerar token de login");
        
            _emailService.SendEmailAsync(new EmailDto
            {
                From = _configuration["SmtpCredentials:Username"],
                To = usuario.Email,
                Subject = $"Token de Login Target: {usuarioToken.TokenLogin}",
                Body = $"Seu token de login para acessar o portal Target é: {usuarioToken.TokenLogin}"
            });

            return Ok(new {
                UserId = usuario.Id,
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao realizar login. {ex.Message}");
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(TokenDto tokenDto)
    {
        try
        {
            var usuario = await _usuarioService.ObterUsuarioPorIdAsync(tokenDto.UserId);
            if(usuario == null) return NotFound("Usuário não encontrado");

            var validation = await _usuarioService.VerificaTokenLogin(usuario, tokenDto.Token);

            switch(validation)
            {
                case TokenValidations.TokenExpirado : return BadRequest("Token expirado");
                case TokenValidations.TokenInvalido : {
                    await _usuarioService.IncrementarTokenTentativas(tokenDto.UserId, usuario);
                    return BadRequest("Token inválido");
                }
                case TokenValidations.TokenValido : {
                    await _usuarioService.LimparTokenTentativas(usuario);
                    break;
                };
            }       

            var tokenDesc = _tokenService.CreateJwtToken(usuario);  
            
            return Ok(
                new
                {
                    tokenDto.UserId,
                    usuario.Name,
                    Token = tokenDesc.Result,
                    ExpiresIn = DateTime.Now.AddDays(1)
                }
            );
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter token de login. Erro: {ex.Message}");
        }
    }
}
