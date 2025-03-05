using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.CTeModels;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Application.Services.CTeService
{
    public class CTeService:ICTeService
    {
        private readonly AppDbContext _context;

        public CTeService(AppDbContext context)
        {
            _context = context;
        }
        public CTe Post(CTeInputModel cteModel)
        {
            CTe cte = cteModel.ToEntity();
            _context.Set<CTe>().Add(cte);
            _context.SaveChanges();
            CTe cteCalculado = CalculaValorBasePorDentro(cte.Id);
            return cteCalculado;
        }
        public List<CTeViewModel> GetAll()
        {
            var ctes = _context.Set<CTe>().Select(c=> new {c.Id,c.ValorCTe,c.ValorICMS,valorFrete = c.Viagem.ValorFrete,c.DataEmissao} ).ToList();   
            List<CTeViewModel> cteModels= ctes.Select(c=>CTeViewModel.FromEntity(c.ValorCTe,c.ValorICMS,c.DataEmissao,c.Id, c.valorFrete)).ToList();   
            return cteModels;
        }
        public CTeViewModelDetails GetById(int id)
        {
            CTe cte = _context.Set<CTe>().SingleOrDefault(c=>c.Id==id);
            CTeViewModelDetails cteModel = CTeViewModelDetails.FromEntity(cte);
            return cteModel;
        }
        public void Delete(int id)
        {
            CTe CTe = _context.Set<CTe>().SingleOrDefault(c => c.Id == id);
            _context.Update(CTe);
            _context.SaveChanges();
        }
        public CTe CalculaValorBaseSimples(int id)
        {
            CTe cte = _context.Set<CTe>().Include(c => c.Viagem).SingleOrDefault(c=>c.Id == id);
            cte.CalculaValorBaseSimples();
            _context.Update(cte);
            _context.SaveChanges();
            return cte;
        }
        public CTe CalculaValorBasePorDentro(int id)
        {
            CTe cte = _context.Set<CTe>().Include(c=>c.Viagem).SingleOrDefault(c => c.Id == id);
            cte.CalculaValorBasePorDentro();
            _context.Update(cte);
            _context.SaveChanges();
            return cte;
        }
    }
}
