using IcmPortal.Core.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Application.Interfaces;
using Target.Domain.Dtos;

namespace Target.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/imagem")]
public class TargetImagemController : ControllerBase
{
    private readonly IImagemService _imagemService;
    private readonly IRelatorioService _relatorioService;
    private readonly IUsuarioLogado _usuarioLogado;
    public TargetImagemController(IImagemService imagemService, IUsuarioLogado usuarioLogado, IRelatorioService relatorioService)
    {
        _imagemService = imagemService;
        _usuarioLogado = usuarioLogado;
        _relatorioService = relatorioService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertImage([FromForm] ImagemDto imagemDto)
    {
        try
        {
            var userId = _usuarioLogado.ObterUsuarioId();

            var imagemId = await _imagemService.InsertImageAsync(imagemDto.FormFile.FileName, userId);
            if (imagemId == null) return NotFound("Imagem não inserida.");

            var storedImage = await _imagemService.StoreImageAsync(imagemId, imagemDto.FormFile);
            if (storedImage == null) return NotFound("Erro ao armazenar imagem.");

            var report = await _relatorioService.InsertClassificationReport(userId, imagemId, 1);
            return Ok(report);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
