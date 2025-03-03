using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Application.Services.ViagemService
{
    public class ViagemService:IViagemService
    {
        private readonly AppDbContext _context;

        public ViagemService(AppDbContext context)
        {
            _context = context;
        }

        public Viagem Post(ViagemInputModel model)
        {
            Viagem viagem = model.ToEntity();
            Carga carga = _context.Set<Carga>().SingleOrDefault(c=>c.Id==model.CargaId);
            viagem.CalculaValorFrete(carga.Peso);
            _context.Set<Viagem>().Add(viagem);
            _context.SaveChanges();
            return viagem;
        }

        public List<ViagemViewModel> GetAll()
        {
            List<Viagem> viagens = _context.Set<Viagem>().Where(v => v.IsDeleted == false).ToList();
            if (viagens == null)  throw new Exception("Viagens não existem");
            List<ViagemViewModel> viagemViewModel = viagens.Select(v => ViagemViewModel.FromEntity(v)).ToList();
            return viagemViewModel;
        }

        public ViagemViewModelDetails GetById(int id)
        {
            Viagem viagem = _context.Set<Viagem>()
                .Where(v => v.IsDeleted == false).Include(v => v.CTe)
                .Include(v => v.Carga).Include(v => v.DespesaAdicionais)
                .SingleOrDefault(v => v.Id == id);
            if (viagem == null) throw new Exception("Viagem não existe");
            ViagemViewModelDetails viagemModel = ViagemViewModelDetails.FromEntity(viagem);
            return viagemModel;
        }

        public void Update(int id, ViagemInputModel viagemModel)
        {
            Viagem viagem = _context.Set<Viagem>()
                .Where(v => v.IsDeleted == false).Include(v=>v.CTe)
                .SingleOrDefault(v => v.Id == id);
           if(viagem.CTe!=null) throw new Exception("Viagem não pode ser Atualizada!");
            if (viagem is null) throw new Exception("Viagem não encontrada!");
            
            Viagem viagemUpdated = viagemModel.ToEntity();

            viagem.Update(viagemUpdated);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Viagem viagem = _context.Set<Viagem>().Where(v => v.IsDeleted == false).SingleOrDefault(v => v.Id == id);
            if (viagem is null) throw new Exception("Viagem não encontrada!");

            viagem.SetAsDeleted();
            _context.Update(viagem);
            _context.SaveChanges();
        }
    }
}
