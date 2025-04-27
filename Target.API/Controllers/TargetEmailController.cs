using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target.Domain.Dtos;
using Target.Application.Helpers;
using Target.Application.Interfaces;
namespace Target.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/email")]
public class TargetEmailController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IEmailService _emailService;
    public TargetEmailController(IUsuarioService usuarioService, IEmailService emailService)
    {
        _usuarioService = usuarioService;
        _emailService = emailService;
    }

    [HttpPost("token")]
    public async Task<IActionResult> SendEmail(EmailDto emailDto)
    {
        try
        {
            _emailService.SendEmailAsync(emailDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
