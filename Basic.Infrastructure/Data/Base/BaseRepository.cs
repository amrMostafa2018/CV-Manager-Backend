using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace Basic.Infrastructure.Data.Base
{
    public class BaseRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public BaseRepository(BasicDbContext dbContext) : base(dbContext)
        {
        }
    }
}
