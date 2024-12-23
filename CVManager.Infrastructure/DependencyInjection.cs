﻿using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using CVManager.Infrastructure.Data;
using CVManager.Infrastructure.Data.Base;
using CVManager.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CVManager.Application.Common.Interfaces;
using CVManager.Application.Interfaces;
using CVManager.Infrastructure.Services;

namespace CVManager.Infrastructure
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            var connectionString = configuration.GetConnectionString("ConnectionString");
            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");
            services.AddDbContext<CVDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ICVDbContext>(provider => provider.GetRequiredService<CVDbContext>());

            services.AddScoped<BasicDbContextInitialiser>();


            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

            services.AddAuthorizationBuilder();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICVManagerService, CVManagerService>();



            return services;
        }
    }
}
