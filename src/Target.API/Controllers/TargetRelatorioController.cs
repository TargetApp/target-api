using System;
using IcmPortal.Core.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Domain.Dtos;
using Target.Application.Interfaces;

namespace Target.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/relatorio")]
    public class TargetRelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;
        private readonly IUsuarioLogado _usuarioLogado;
        public TargetRelatorioController(IRelatorioService relatorioService, IUsuarioLogado usuarioLogado)
        {
            _usuarioLogado = usuarioLogado;
            _relatorioService = relatorioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterRelatorioPorUsuarioId()
        {
            try
            {
                var usuarioId = 1;
                var relatorio = await _relatorioService.ObterRelatoriosPorUsuarioIdAsync(usuarioId);
                if (relatorio.Count == 0) return NotFound("Relatório não encontrado.");

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarRelatorio(RelatorioCriarDto relatorioDto)
        {
            try
            {
                var usuarioId = _usuarioLogado.ObterUsuarioId();
                var relatorio = await _relatorioService.AdicionarRelatorioAsync(relatorioDto, usuarioId);
                if (relatorio == null) return NotFound("Relatório não encontrado.");

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{relatorioId}")]
        public async Task<IActionResult> AtualizarRelatorio(int relatorioId, RelatorioDto relatorioDto)
        {
            try
            {
                var relatorio = await _relatorioService.AtualizarRelatorioAsync(relatorioId, relatorioDto);
                if (relatorio == null) return NotFound("Relatório não encontrado.");

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{relatorioId}")]
        public async Task<IActionResult> ExcluirRelatorio(int relatorioId)
        {
            try
            {
                var relatorio = await _relatorioService.ExcluirRelatorioAsync(relatorioId);
                if (relatorio == null) return NotFound("Relatório não encontrado.");

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
