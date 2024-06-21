using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Target.Application.Interfaces;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Application.Services;

public class ImagemService : IImagemService
{
    private readonly IGeralPersist _geralRepository;
    private readonly IStoragePersist _storageRepository;
    public ImagemService(IGeralPersist geralRepository, IStoragePersist storageRepository)
    {
        _geralRepository = geralRepository;
        _storageRepository = storageRepository;
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

    public async Task<ImageStorage> StoreImageAsync(int imageId, IFormFile formFile)
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
            var image = new ImageStorage
            {
                Id = imageId,
                Data = byteImage
            };

            _storageRepository.Add(image);
            await _storageRepository.SaveChangesAsync();

            return image;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
