using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Infra.Repostories.DespesasAdicionaisRepositories
{
    public class DespesasRepository:IDespesasRepository
    {
        private readonly AppDbContext _context;

        public DespesasRepository(AppDbContext context)
        {
            _context = context;
        }

        public DespesaAdicional Post(DespesaAdicional despesa)
        {
            _context.Set<DespesaAdicional>().Add(despesa);
            _context.SaveChanges();
            return despesa;
        }

        public List<DespesaAdicional> GetAll()
        {
            List<DespesaAdicional> despesas = _context.Set<DespesaAdicional>().ToList();
            return despesas;
        }

        public DespesaAdicional GetById(int id)
        {
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().Include(d => d.Viagem).ThenInclude(v=>v.CTe).SingleOrDefault(v => v.Id == id);
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            return despesa;
        }

        public void Update(DespesaAdicional despesaUpdated)
        {
            _context.Update(despesaUpdated);
            _context.SaveChanges();
        }

        public void Delete(DespesaAdicional despesa)
        {
            _context.Remove(despesa);
            _context.SaveChanges();
        }
    }
}
