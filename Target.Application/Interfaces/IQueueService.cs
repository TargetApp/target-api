using Target.Domain.Dtos;

namespace Target.Application.Interfaces;

public interface IQueueService
{
    Task<bool> EnqueueAsync(QueueDto queueDto);
}
