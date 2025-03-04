using Microsoft.Extensions.DependencyInjection;
using RegistrosCTe.Application.Services.CargaServices;
using RegistrosCTe.Application.Services.CTeService;
using RegistrosCTe.Application.Services.DespesasServices;
using RegistrosCTe.Application.Services.ViagemService;

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
            services.AddScoped<IViagemService, ViagemService>();
            services.AddScoped<ICargaService, CargaService>();
            services.AddScoped<IDespesasService, DespesasService>();
            services.AddScoped<ICTeService, CTeService>();
            return services;
        }
    }
}
