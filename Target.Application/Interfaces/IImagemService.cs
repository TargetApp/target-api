using Microsoft.AspNetCore.Http;
using Target.Domain.Models;

namespace Target.Application.Interfaces;

public interface IImagemService
{
    Task<int> InsertImageAsync(string filename, int userId);
    Task<ImageStorage> StoreImageAsync(int imageId, IFormFile imageBytes);
}
