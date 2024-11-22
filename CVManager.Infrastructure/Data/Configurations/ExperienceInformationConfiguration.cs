using CVManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.Infrastructure.Data.Configurations
{
    public class ExperienceInformationConfiguration : IEntityTypeConfiguration<ExperienceInformation>
    {
        public void Configure(EntityTypeBuilder<ExperienceInformation> builder)
        {
            builder.Property(t => t.CompanyName).IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.City)
                   .HasMaxLength(200);

            builder.Property(t => t.CompanyField)
                   .HasMaxLength(200);
        }
    }
}
