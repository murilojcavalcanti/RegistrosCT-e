using Microsoft.EntityFrameworkCore;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Persistance
{
    public class AppDbContext : DbContext
    {
        protected AppDbContext(DbContextOptions<AppDbContext>opts):base(opts) { }

        DbSet<Viagem> Viagems { get; set; }
        DbSet<Carga> Carga { get; set; }
        DbSet<DespesaAdicional> DespesasAdicionais { get; set; }
        
    }
}
