using Target.Application.Interfaces;
using Target.Domain.Dtos;
using Target.Domain.Models;
using Target.Persistence.Interfaces;

namespace Target.Application.Services;

public class QueueService : IQueueService
{
    private readonly IQueuePersist _queueRepository;
    public QueueService(IQueuePersist queueRepository)
    {
        _queueRepository = queueRepository;
    }

    public async Task<bool> EnqueueAsync(QueueDto queueDto)
    {
        try
        {
            var queue = new TargetQueue
            {
                ImageId = queueDto.ImageId,
                ModelId = queueDto.ModelId,
                ClassificationReportId = queueDto.ReportId,
                Image = queueDto.Image,
                GenerateMask = queueDto.GenerateMask
            };

            _queueRepository.Add(queue);
            await _queueRepository.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
