using Microsoft.AspNetCore.Http;
namespace Target.Domain.Dtos;

public class ImagemDto
{
    public int AIModel { get; set; }
    public IFormFile FormFile { get; set; }
}
