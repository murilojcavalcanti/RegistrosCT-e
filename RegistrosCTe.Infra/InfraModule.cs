using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Infra.Repostories.CargaRepositories;
using RegistrosCTe.Infra.Repostories.CTeRepositories;
using RegistrosCTe.Infra.Repostories.DespesasAdicionaisRepositories;
using RegistrosCTe.Infra.Repostories.ViagemRepositories;

namespace RegistrosCTe.Infra
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddData(configuration).AddRepository();
            return services;
        }
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            string conString = configuration.GetConnectionString("cteApp");
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(conString));
            return services;
        }
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICargaRepository,CargaRepository>();
            services.AddScoped<IViagemRepository,ViagemRepository>();
            services.AddScoped<IDespesasRepository, DespesasRepository>();
            services.AddScoped<ICTeRepository, CTeRepository>();
            return services;
        }
    }
}
