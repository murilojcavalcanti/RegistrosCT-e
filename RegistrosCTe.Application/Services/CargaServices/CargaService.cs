
using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Application.Services.CargaServices
{
    public class CargaService:ICargaService
    {
        private readonly AppDbContext _context;

        public CargaService(AppDbContext context)
        {
            _context = context;
        }
        public Carga Post(CargaInputModel cargaModel)
        {
            Carga carga = cargaModel.ToEntity();
            _context.Set<Carga>().Add(carga);
            _context.SaveChanges();
            return carga;
        }

        public List<CargaViewModel> GetAll()
        {
            List<Carga> cargas = _context.Set<Carga>().AsNoTracking().Where(c => c.IsDeleted == false).ToList();
            if (cargas is null) throw new Exception("Cargas não encontradas");
            List<CargaViewModel> cargasModel = cargas.Select(c => CargaViewModel.FromEntity(c)).ToList();
            return cargasModel;
        }

        public CargaViewModelDetails GetById(int id)
        {
            Carga carga = _context.Set<Carga>().Where(c => c.IsDeleted == false).Include(c => c.Viagem).SingleOrDefault(v => v.Id == id);
            if (carga is null) throw new Exception("Carga não encontrada!");
            CargaViewModelDetails cargaModel = CargaViewModelDetails.FromEntity(carga);
            return cargaModel;
        }

        public void Update(int id, CargaInputModel cargaModel)
        {
            Carga carga = _context.Set<Carga>().Include(c=>c.Viagem).SingleOrDefault(v => v.Id == id);
            if (carga.Viagem != null) throw new Exception("Carga não pode ser atualizada!");
            Carga cargaUpdated = cargaModel.ToEntity();
            if (carga is null) throw new Exception("Carga não encontrada!");
            carga.Update(cargaUpdated);
            _context.SaveChanges();
        }

        
        public void Delete(int id)
        {
            Carga carga = _context.Set<Carga>().Include(c=>c.Viagem).ThenInclude(v=>v.CTe).SingleOrDefault(v => v.Id == id);
            if (carga.Viagem != null||carga.Viagem.CTe!=null) throw new Exception("Carga não pode ser Atualiada!");

            if (carga is null) throw new Exception("Carga não encontrada!");
            carga.SetAsDeleted();
            _context.Update(carga);
            _context.SaveChanges();
        }

    }
}
