using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Basic.Application.Interfaces;
using Basic.Infrastructure.Data;
using Basic.Infrastructure.Data.Base;
using Basic.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Basic.Infrastructure
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            var connectionString = configuration.GetConnectionString("ConnectionString");
            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");
            services.AddDbContext<BasicDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IBasicDbContext>(provider => provider.GetRequiredService<BasicDbContext>());

            services.AddScoped<BasicDbContextInitialiser>();


            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

            services.AddAuthorizationBuilder();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(BaseRepository<>));
            //services.AddScoped<IPayService, PayService>();



            return services;
        }
    }
}
