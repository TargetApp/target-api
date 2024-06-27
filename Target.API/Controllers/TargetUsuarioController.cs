using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Domain.Dtos;
using Target.Application.Interfaces;

namespace Target.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/usuario")]
    public class TargetUsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public TargetUsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObterUsuarioPorId(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioService.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null) return NotFound("Nenhum usuário encontrado.");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);	
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(UsuarioCadastroDto model)
        {
            try
            {
                var usuario = await _usuarioService.AdicionarUsuario(model);
                if (usuario == null) return NotFound("Erro ao tentar adicionar usuário");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);	
            }
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> AtualizarUsuario(int usuarioId, UsuarioUpdateDto model)
        {
            try
            {
                var usuario = await _usuarioService.AtualizarUsuario(usuarioId, model);
                if (usuario == null) return NotFound("Erro ao tentar atualizar usuário. Esse usuário não existe.");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);	
            }
        }

        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> ExcluirUsuario(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioService.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null) return NotFound("Erro ao tentar excluir usuário. Esse usuário não existe.");

                return await _usuarioService.DeletarUsuario(usuarioId) ? 
                    Ok("Usuário excluído com sucesso") : 
                    BadRequest("Erro ao tentar excluir usuário");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);	
            }
        }
    }
}
