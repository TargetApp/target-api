using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Application.Dtos;
using Target.Application.Helpers;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence;
namespace Target.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
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
            var usuario = await _usuarioService.ObterUsuarioCadastradoAsync(model.Email, model.Telefone);
            if (usuario == null) usuario = await this._usuarioService.AdicionarUsuario(model);

            var usuarioToken = await _usuarioService.AtualizarTokenLogin(usuario.Id);
            if (usuarioToken == null) return BadRequest("Erro ao gerar token de login");
            //enviar codigo via email ou sms

            return Ok(usuario.Id);
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
            var usuario = await _usuarioService.ObterUsuarioPorIdAsync(tokenDto.UsuarioId);
            if(usuario == null) return NotFound("Usuário não encontrado");

            var validation = await _usuarioService.VerificaTokenLogin(usuario, tokenDto.Token);
            _usuarioService.IncrementarTokenTentativas(tokenDto.UsuarioId);

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
                    nome = usuario.Nome,
                    token = new {
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

    [HttpPut("atualizar-usuario/{id}")]
    public async Task<IActionResult> AtualizarUsuario(int id, UsuarioUpdateDto model)
    {
        try
        {
            var usuario = await _usuarioService.AtualizarUsuario(id, model);
            if (usuario == null) return BadRequest("Erro ao atualizar usuário");

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar usuário. Erro: {ex.Message}");
        }
    }

    [HttpDelete("deletar-usuario/{id}")]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        try
        {
            return await _usuarioService.DeletarUsuario(id) ?
                Ok("Usuário deletado com sucesso") :
                BadRequest("Erro ao deletar usuário");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao deletar usuário. Erro: {ex.Message}");
        }
    }
}
