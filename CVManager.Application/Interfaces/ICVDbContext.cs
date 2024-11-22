

namespace CVManager.Application.Interfaces
{
    public interface ICVDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
