using Basic.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Basic.Infrastructure.Data
{
    public class BasicDbContext : DbContext, IBasicDbContext
    {
        public BasicDbContext(DbContextOptions<BasicDbContext> options) : base(options)
        {
        }

        //public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
