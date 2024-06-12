using Target.Application.Interfaces;
using Target.Domain.Dtos;

namespace Target.Application.Services;

public class QueueService : IQueueService
{
    // private readonly IQueueRepository _queueRepository;
    // public QueueService(IQueueRepository queueRepository)
    // {
    //     _queueRepository = queueRepository;
    // }

    public Task<bool> EnqueueAsync(QueueDto queueDto)
    {
        throw new NotImplementedException();
    }

}
