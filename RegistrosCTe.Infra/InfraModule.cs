using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrosCTe.API.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Infra
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddData(configuration);
            return services;
        }
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            string conString = configuration.GetConnectionString("cteApp");
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(conString));
            return services;
        }
    }
}
