using Microsoft.AspNetCore.Mvc;
using Target.Application.Interfaces;
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
}
