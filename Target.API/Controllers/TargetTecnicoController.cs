using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Domain.Dtos;
using Target.Application.Interfaces;
using IcmPortal.Core.Dominio.Interfaces;

namespace Target.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/tecnico")]
    public class TargetTecnicoController : ControllerBase
    {
        private readonly ITecnicoService _tecnicoService;
        private readonly IUsuarioLogado _usuarioLogado;
        public TargetTecnicoController(ITecnicoService tecnicoService, IUsuarioLogado usuarioLogado)
        {
            _tecnicoService = tecnicoService;
            _usuarioLogado = usuarioLogado;
        }

        [HttpGet]
        public async Task<IActionResult> ObterListaTecnicos([FromQuery] string? filter)
        {
            try
            {
                var tecnicos = await _tecnicoService.ObterListaTecnicosAsync(filter);
                if (tecnicos == null) return NotFound("Nenhum técnico encontrado.");

                return Ok(tecnicos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tecnicoId}")]
        public async Task<IActionResult> ObterTecnicoPorId(int tecnicoId)
        {
            try
            {
                var tecnico = await _tecnicoService.ObterTecnicoAsyncById(tecnicoId);
                if (tecnico == null) return NotFound("Nenhum técnico encontrado.");

                return Ok(tecnico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTecnico(TecnicoDto model)
        {
            try
            {
                var usuarioId = _usuarioLogado.ObterUsuarioId();
                var tecnico = await _tecnicoService.AdicionarTecnicoAsync(model, usuarioId);
                if (tecnico == null) return NotFound("Erro ao adicionar técnico. Verifique os dados e tente novamente.");

                return Ok(tecnico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{isEvaluation}/{tecnicoId}")]
        public async Task<IActionResult> AtualizarTecnico(int tecnicoId, TecnicoAtualizarDto model, bool isEvaluation = false)
        {
            try
            {
                var tecnico = await _tecnicoService.AtualizarTecnicoAsync(tecnicoId, model, isEvaluation);
                if (tecnico == null) return NotFound("Erro ao atualizar técnico. Verifique os dados e tente novamente.");

                return Ok(tecnico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{tecnicoId}")]
        public async Task<IActionResult> ExcluirTecnico(int tecnicoId)
        {
            try
            {
                var tecnico = await _tecnicoService.ObterTecnicoAsyncById(tecnicoId);
                if (tecnico == null) return NotFound("Nenhum técnico encontrado.");

                await _tecnicoService.ExcluirTecnicoAsync(tecnicoId);

                return Ok("Técnico excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
