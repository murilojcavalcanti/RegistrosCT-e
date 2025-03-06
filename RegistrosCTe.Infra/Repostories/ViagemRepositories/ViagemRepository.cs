using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Infra.Repostories.ViagemRepositories
{
    public class ViagemRepository:IViagemRepository
    {
        private readonly AppDbContext _context;

        public ViagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public Viagem Post(Viagem viagem)
        {
            _context.Set<Viagem>().Add(viagem);
            _context.SaveChanges();
            return viagem;
        }

        public List<Viagem> GetAll()
        {
            List<Viagem> viagens = _context.Set<Viagem>().ToList();
            if (viagens == null) throw new Exception("Viagens não existem");
            return viagens;
        }

        public Viagem GetById(int id)
        {
            Viagem viagem = _context.Set<Viagem>()
                .Include(v => v.CTe)
                .Include(v => v.Carga).Include(v => v.DespesaAdicionais)
                .SingleOrDefault(v => v.Id == id);
            if (viagem == null) throw new Exception("Viagem não existe");
            return viagem;
        }

        public void Update(Viagem viagemUpdated)
        {
            _context.Update(viagemUpdated);
            _context.SaveChanges();
        }

        public void Delete(Viagem viagem)
        {   
            _context.Remove(viagem);
            _context.SaveChanges();
        }
    }
}
