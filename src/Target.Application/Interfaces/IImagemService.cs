using Microsoft.AspNetCore.Http;

namespace Target.Application.Interfaces;

public interface IImagemService
{
    Task<int> InsertImageAsync(string filename, int userId);
    Task<bool> StoreImageAsync(int imageId, IFormFile imageBytes);
}
