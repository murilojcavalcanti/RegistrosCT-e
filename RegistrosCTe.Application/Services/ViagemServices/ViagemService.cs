using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.CargaRepositories;
using RegistrosCTe.Infra.Repostories.ViagemRepositories;

namespace RegistrosCTe.Application.Services.ViagemService
{
    public class ViagemService:IViagemService
    {
        private readonly AppDbContext _context;
        private readonly IViagemRepository _ViagemRepository;
        public ViagemService(IViagemRepository viagemRepository, ICargaRepository cargaRepository)
        {
            _ViagemRepository = viagemRepository;
        }

        public ViagemViewModel Post(ViagemInputModel model)
        {
            Viagem viagem = model.ToEntity();
            _ViagemRepository.Post(viagem);
            ViagemViewModel viagemCreated = ViagemViewModel.FromEntity(CalculaValorFrete(viagem.Id));
            return viagemCreated;
        }

        public List<ViagemViewModel> GetAll()
        {
            List<Viagem> viagens = _ViagemRepository.GetAll();
            if (viagens == null)  throw new Exception("Viagens não existem");
            List<ViagemViewModel> viagemViewModel = viagens.Select(v => ViagemViewModel.FromEntity(v)).ToList();
            return viagemViewModel;
        }

        public ViagemViewModelDetails GetById(int id)
        {
            Viagem viagem = _ViagemRepository.GetById(id);
            if (viagem == null) throw new Exception("Viagem não existe");
            ViagemViewModelDetails viagemModel = ViagemViewModelDetails.FromEntity(viagem);
            return viagemModel;
        }

        public void Update(int id, ViagemUpdateInputModel viagemModel)
        {
            Viagem viagemUpdated = viagemModel.ToEntity();
            Viagem viagem = _ViagemRepository.GetById(id);
            if (viagem is null) throw new Exception("Viagem não encontrada!");
            if (viagem.CTe != null) throw new Exception("Viagem não pode ser Atualizada!");
            viagem.Update(viagemUpdated);
            _ViagemRepository.Update(viagem);

            
        }

        public void Delete(int id)
        {
            Viagem viagem = _ViagemRepository.GetById(id);
            if (viagem is null) throw new Exception("Viagem não encontrada!");

            _ViagemRepository.Delete(viagem);
        }

        public Viagem CalculaValorFrete(int id)
        {
            Viagem viagem = _ViagemRepository.GetById(id);
            viagem.CalculaValorFrete();
            _ViagemRepository.Update(viagem);
            return viagem;
        }
        public Viagem RecalculaValorFrete(int id, decimal despesa)
        {
            Viagem viagem = _ViagemRepository.GetById(id);
            viagem.RecalculaValorFrete(despesa);
            _ViagemRepository.Update(viagem);
            return viagem;
        }
    }
}
