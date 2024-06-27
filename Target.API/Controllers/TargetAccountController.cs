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
    public TargetAccountController(IUsuarioService usuarioService, ITokenService tokenService)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

    // [HttpGet("usuarios-cadastrados")]
    // public async Task<IActionResult> ObterUsuariosCadastrados()
    // {
    //     try
    //     {
    //         var usuarios = await _usuarioService.();
    //         if (usuarios == null) return NotFound("Nenhum usuário encontrado");

    //         return Ok(usuarios);
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception($"Erro ao obter usuarios cadastrados. Erro: {ex.Message}");
    //     }
    // }

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
            //enviar codigo via email ou sms

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
                    ExpiresIn = 3600
                }
            );
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter token de login. Erro: {ex.Message}");
        }
    }
}
