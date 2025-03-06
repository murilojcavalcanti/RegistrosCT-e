using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Infra.Repostories.CargaRepositories
{
    public class CargaRepository:ICargaRepository
    {
        private readonly AppDbContext _context;

        public CargaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Carga Post(Carga carga)
        {
            _context.Set<Carga>().Add(carga);
            _context.SaveChanges();
            return carga;
        }

        public List<Carga> GetAll()
        {
            List<Carga> cargas = _context.Set<Carga>().AsNoTracking().ToList();
            if (cargas is null) throw new Exception("Cargas não encontradas");
            return cargas;
        }

        public Carga GetById(int id)
        {
            Carga carga = _context.Set<Carga>().Include(c => c.Viagem).SingleOrDefault(v => v.Id == id);
            if (carga is null) throw new Exception("Carga não encontrada!");
            return carga;
        }

        public void Update(int id, Carga cargaUpdated)
        {
            Carga carga = _context.Set<Carga>().SingleOrDefault(v => v.Id == id);
            if (carga is null) throw new Exception("Carga não encontrada!");
            if (carga.Viagem != null) throw new Exception("Carga não pode ser atualizada!");
            carga.Update(cargaUpdated);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            Carga carga = _context.Set<Carga>().Include(c => c.Viagem).SingleOrDefault(v => v.Id == id);
            if (carga is null) throw new Exception("Carga não encontrada!");
            if (carga?.Viagem != null) throw new Exception("Carga não pode ser Deletada!");

            _context.Remove<Carga>(carga);
            _context.SaveChanges();

        }

    }
}
