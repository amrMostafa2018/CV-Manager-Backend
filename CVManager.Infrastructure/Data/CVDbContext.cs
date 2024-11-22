using CVManager.Application.Interfaces;
using CVManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace CVManager.Infrastructure.Data
{
    public class CVDbContext : DbContext, ICVDbContext
    {
        public CVDbContext(DbContextOptions<CVDbContext> options) : base(options)
        {
        }

        public DbSet<CV> CVs { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure CV & Personal_Information entity
            builder.Entity<CV>()
                   .HasOne(p => p.PersonalInformation)
                   .WithOne(c => c.Cv)
                   .HasForeignKey<PersonalInformation>(p => p.PersonalInformationId);

            // Configure CV & ExperienceInformation entity
            builder.Entity<CV>()
                   .HasOne(p => p.ExperienceInformation)
                   .WithOne(c => c.Cv)
                   .HasForeignKey<ExperienceInformation>(p => p.ExperienceInformationId);

            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
