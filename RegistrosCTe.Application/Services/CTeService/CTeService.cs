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
            CalculaValorBasePorDentro(cte.Id);
            return cte;
        }
        public List<CTeViewModel> GetAll()
        {
            List<CTe> ctes = _context.Set<CTe>().Where(c=>c.IsDeleted == false).ToList();   
            List<CTeViewModel> cteModels= ctes.Select(c=>CTeViewModel.FromEntity(c)).ToList();   
            return cteModels;
        }
        public CTeViewModelDetails GetById(int id)
        {
            CTe cte = _context.Set<CTe>().Where(c => c.IsDeleted == false).SingleOrDefault(c=>c.Id==id);
            CTeViewModelDetails cteModel = CTeViewModelDetails.FromEntity(cte);
            return cteModel;
        }
        public void Delete(int id)
        {
            CTe CTe = _context.Set<CTe>().Where(c => c.IsDeleted == false).SingleOrDefault(c => c.Id == id);
            CTe.SetAsDeleted();
            _context.Update(CTe);
            _context.SaveChanges();
        }
        public CTe CalculaValorBaseSimples(int id)
        {
            CTe cte = _context.Set<CTe>().Include(c => c.Viagem).Where(c=>c.IsDeleted==false).SingleOrDefault(c=>c.Id == id);
            cte.CalculaValorBaseSimples();
            _context.Update(cte);
            _context.SaveChanges();
            return cte;
        }
        public CTe CalculaValorBasePorDentro(int id)
        {
            CTe cte = _context.Set<CTe>().Include(c => c.Viagem).Where(c => c.IsDeleted == false).SingleOrDefault(c => c.Id == id);
            cte.CalculaValorBasePorDentro();
            _context.Update(cte);
            _context.SaveChanges();
            return cte;
        }
    }
}
