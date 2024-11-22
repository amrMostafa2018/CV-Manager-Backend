

namespace Basic.Application.Interfaces
{
    public interface IBasicDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
