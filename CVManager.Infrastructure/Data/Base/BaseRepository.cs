using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace CVManager.Infrastructure.Data.Base
{
    public class BaseRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public BaseRepository(CVDbContext dbContext) : base(dbContext)
        {
        }
    }
}
