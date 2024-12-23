﻿// <auto-generated />
using System;
using CVManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CVManager.Infrastructure.Migrations
{
    [DbContext(typeof(CVDbContext))]
    partial class CVDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CVManager.Domain.Entities.CV", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("CVs");
                });

            modelBuilder.Entity("CVManager.Domain.Entities.ExperienceInformation", b =>
                {
                    b.Property<int>("ExperienceInformationId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CompanyField")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ExperienceInformationId");

                    b.ToTable("ExperienceInformation");
                });

            modelBuilder.Entity("CVManager.Domain.Entities.PersonalInformation", b =>
                {
                    b.Property<int>("PersonalInformationId")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PersonalInformationId");

                    b.ToTable("PersonalInformation");
                });

            modelBuilder.Entity("CVManager.Domain.Entities.ExperienceInformation", b =>
                {
                    b.HasOne("CVManager.Domain.Entities.CV", "Cv")
                        .WithOne("ExperienceInformation")
                        .HasForeignKey("CVManager.Domain.Entities.ExperienceInformation", "ExperienceInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("CVManager.Domain.Entities.PersonalInformation", b =>
                {
                    b.HasOne("CVManager.Domain.Entities.CV", "Cv")
                        .WithOne("PersonalInformation")
                        .HasForeignKey("CVManager.Domain.Entities.PersonalInformation", "PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("CVManager.Domain.Entities.CV", b =>
                {
                    b.Navigation("ExperienceInformation")
                        .IsRequired();

                    b.Navigation("PersonalInformation")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
