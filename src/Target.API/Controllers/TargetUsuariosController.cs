using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Application.Dtos;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence;

namespace Target.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TargetApiController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly TargetDbContext _context;
    public TargetApiController(TargetDbContext context,
                                IUsuarioService usuarioService)
    {
        _context = context;
        _usuarioService = usuarioService;
    }

    [HttpGet("usuarios-cadastrados")]
    public async Task<IActionResult> ObterUsuariosCadastrados()
    {
        try
        {
            var usuarios = await _usuarioService.ObterListaUsuariosAsync();
            if (usuarios == null) return NotFound("Nenhum usuário encontrado");

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter usuarios cadastrados. Erro: {ex.Message}");
        }
    }

    [HttpGet("usuario/{id}")]
    public async Task<IActionResult> ObterUsuarioPorId(int id)
    {
        try
        {
            var usuario = await _usuarioService.ObterUsuarioPorIdAsync(id);
            if (usuario == null) return NotFound("Nenhum usuário encontrado");

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter usuario por id. Erro: {ex.Message}");
        }
    }

    [HttpPost("cadastro")]
    [AllowAnonymous]
    public async Task<IActionResult> AdicionarUsuario(UsuarioDto model)
    {
        try
        {
            if(await _usuarioService.UsuarioExiste(model.Email)) 
                return BadRequest("Usuário já cadastrado");

            var usuario = await _usuarioService.AdicionarUsuario(model);
            if (usuario == null) return BadRequest("Erro ao cadastrar usuário");

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao cadastrar usuário. Erro: {ex.Message}");
        }
    }

    [HttpPut("atualizar-usuario/{id}")]
    public async Task<IActionResult> AtualizarUsuario(int id, UsuarioDto model)
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
