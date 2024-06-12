using Microsoft.AspNetCore.Http;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Application.Services;

public class ImagemService : IImagemService
{
    private readonly IQueuePersist _queueRepository;
    private readonly IImagemPersist _imagemRepository;
    private readonly IGeralPersist _geralRepository;
    public ImagemService(IQueuePersist queueRepository, IImagemPersist imagemRepository, IGeralPersist geralRepository)
    {
        _queueRepository = queueRepository;
        _imagemRepository = imagemRepository;
        _geralRepository = geralRepository;
    }

    public async Task<int> InsertImageAsync(string filename, int userId)
    {
        try
        {
            var imagem = new Imagens
            {
                Filename = filename,
                UserId = userId,
                UploadedAt = DateTime.Now
            };

            _geralRepository.Add(imagem);
            await _geralRepository.SaveChangesAsync();

            return imagem.Id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> StoreImageAsync(int imageId, IFormFile formFile)
    {
        byte[] byteImage;

        using (var memoryStream = new MemoryStream())
        {
            await formFile.CopyToAsync(memoryStream);
            byteImage = memoryStream.ToArray();

            memoryStream.Close();
        }

        try
        {
           // _geralRepository.Add<ImageStorage>(byteImage);
            return await _geralRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
