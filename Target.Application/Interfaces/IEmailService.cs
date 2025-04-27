using Target.Domain.Dtos;

namespace Target.Application.Interfaces;

public interface IEmailService
{
    void SendEmailAsync(EmailDto emailDto);
}
