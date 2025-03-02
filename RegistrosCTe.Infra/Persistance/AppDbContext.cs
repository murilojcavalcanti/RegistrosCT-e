using Microsoft.EntityFrameworkCore;
using RegistrosCTe.Domain.Entities;
using System.Reflection.Emit;

namespace RegistrosCTe.API.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>opts):base(opts) { }
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<DespesaAdicional>(e =>
            {
                e.HasOne(d => d.Viagem)
                .WithMany(v => v.DespesaAdicionais)
                .HasForeignKey(d => d.ViagemId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            Builder.Entity<Viagem>(e =>
            {
                e.HasOne(v => v.Carga)
                .WithOne(c => c.Viagem)
                .HasForeignKey<Viagem>(v => v.CargaId)
                .OnDelete(DeleteBehavior.Restrict);
           });
        }

        DbSet<CTe> CTe { get; set; }
        DbSet<Carga> Carga { get; set; }
        DbSet<Viagem> Viagems { get; set; }
        DbSet<DespesaAdicional> DespesasAdicionais { get; set; }

        
    }
}
