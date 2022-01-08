using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMS.Profile.Application.Common.Interfaces;
using OMS.Profile.Infrastructure.Persistence;

namespace OMS.Profile.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(
                    connectionString,
                    x => { x.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName); })
                .EnableSensitiveDataLogging();
        });

        services.AddScoped<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
    }
}