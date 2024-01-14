using Galaxi.Functions.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FunctionContextDb>(options =>
                options.UseSqlServer(
                    configuration["connectionStrings:MovieFunctionsEntities"] ?? string.Empty,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(FunctionContextDb).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null
                        );
                    }));
            services.AddScoped<IFunctionContextDb>(provider => provider.GetRequiredService<FunctionContextDb>());
            return services;
        }
    }
}
