using IcmPortal.Core.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Application.Interfaces;
using Target.Domain.Dtos;
using Target.Domain.Enum;

namespace Target.API.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]/imagem")]
public class TargetImagemController : ControllerBase
{
    private readonly IImagemService _imagemService;
    private readonly IQueueService _queueService;
    private readonly IRelatorioService _relatorioService;
    private readonly IUsuarioLogado _usuarioLogado;
    public TargetImagemController(IImagemService imagemService, IUsuarioLogado usuarioLogado, IRelatorioService relatorioService, IQueueService queueService)
    {
        _imagemService = imagemService;
        _usuarioLogado = usuarioLogado;
        _relatorioService = relatorioService;
        _queueService = queueService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertImage([FromForm] ImagemDto imagemDto)
    {
        try
        {
            var userId = 1;

            var imagemId = await _imagemService.InsertImageAsync(imagemDto.FormFile.FileName, userId);
            if (imagemId == null) return NotFound("Imagem não inserida.");

            var storedImage = await _imagemService.StoreImageAsync(imagemId, imagemDto.FormFile);
            if (storedImage == null) return NotFound("Erro ao armazenar imagem.");

            var reportId = await _relatorioService.InsertClassificationReport(userId, imagemId, 1);
           
            var enqueued = await _queueService.EnqueueAsync(new QueueDto 
            { 
                ImageId = imagemId,
                ModelId = 1,
                ReportId = reportId,
                Image = storedImage.Data,
                GenerateMask = false
            });

            return Ok(new {
                Enqueued = enqueued,
                ImageId = imagemId,
                ReportId = reportId
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
