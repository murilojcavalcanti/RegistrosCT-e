using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Services.ViagemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddServices();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IViagemService,ViagemService>();
             return services;
        }
    }
}
