using Target.Persistence.Interfaces;

namespace Target.Persistence.Persistences;

public class QueuePersist : IQueuePersist
{
    private readonly QueueDbContext _context;
    public QueuePersist(QueueDbContext context)
    {
        _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public void DeleteRange<T>(T[] entityArray) where T : class
    {
        _context.RemoveRange(entityArray);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }

}
