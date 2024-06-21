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
            var usuario = await _usuarioService.ObterUsuarioCadastradoAsync(model.Email, model.Telephone);
            if (usuario == null) usuario = await _usuarioService.AdicionarUsuario(model);

            var usuarioToken = await _usuarioService.AtualizarTokenLogin(usuario.Id);
            if (usuarioToken == null) return NotFound("Erro ao gerar token de login");
            //enviar codigo via email ou sms

            return Ok(new {
                UserId = usuario.Id,
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao realizar login. Erro: {ex.Message}");
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
            _usuarioService.IncrementarTokenTentativas(tokenDto.UserId, usuario);

            switch(validation)
            {
                case TokenValidations.TokenExpirado : return BadRequest("Token expirado");
                case TokenValidations.TokenInvalido : return BadRequest("Token inválido");
                case TokenValidations.TokenValido : break;
            }       

            var tokenDesc = _tokenService.CreateJwtToken(usuario);  
            
            return Ok(
                new
                {
                    Id = tokenDto.UserId,
                    Nome = usuario.Name,
                    Token = new {
                       jwt = tokenDesc.Result,
                       tempoExpiracao = "3600"
                    }
                }
            );
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter token de login. Erro: {ex.Message}");
        }
    }
}
