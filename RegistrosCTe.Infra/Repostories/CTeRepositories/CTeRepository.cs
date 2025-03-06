using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Infra.Repostories.CTeRepositories
{
    public class CTeRepository:ICTeRepository
    {
        private readonly AppDbContext _context;

        public CTeRepository(AppDbContext context)
        {
            _context = context;
        }

        public CTe Post(CTe cte)
        {
            _context.Set<CTe>().Add(cte);
            _context.SaveChanges();
            return cte;
        }
        public List<CTe> GetAll()
        {
            var ctes = _context.Set<CTe>().Include(c=>c.Viagem).ToList();
            return ctes;
        }
        public CTe GetById(int id)
        {
            CTe cte = _context.Set<CTe>()
                .Include(c=>c.Viagem).ThenInclude(c=>c.Carga)
                .Include(c => c.Viagem).ThenInclude(c => c.DespesaAdicionais)
                .SingleOrDefault(c => c.Id == id);
            return cte;
        }
        public CTe Update(CTe cteUpdated)
        {
            _context.Update(cteUpdated);
            _context.SaveChanges();
            return cteUpdated;
        }
        public void Delete(CTe cte)
        {
            _context.Remove(cte);
            _context.SaveChanges();
        }
    }
}
