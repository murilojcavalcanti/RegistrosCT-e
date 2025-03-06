using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Application.Services.CTeService;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.CargaRepositories;
using RegistrosCTe.Infra.Repostories.ViagemRepositories;

namespace RegistrosCTe.Application.Services.ViagemService
{
    public class ViagemService : IViagemService
    {
        private readonly AppDbContext _context;
        private readonly IViagemRepository _ViagemRepository;
        private readonly ICTeService _CTeService;
        public ViagemService(IViagemRepository viagemRepository, ICargaRepository cargaRepository, ICTeService cTeService)
        {
            _ViagemRepository = viagemRepository;
            _CTeService = cTeService;
        }

        public ViagemViewModel Post(ViagemInputModel model)
        {
            try
            {
                Viagem viagem = model.ToEntity();
                _ViagemRepository.Post(viagem);
                ViagemViewModel viagemCreated = ViagemViewModel.FromEntity(CalculaValorFrete(viagem.Id));
                return viagemCreated;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task<List<ViagemViewModel>> GetAll()
        {
            try
            {
                List<Viagem> viagens = await _ViagemRepository.GetAll();
                if (viagens == null) throw new Exception("Viagens não existem");
                List<ViagemViewModel> viagemViewModel = viagens.Select(v => ViagemViewModel.FromEntity(v)).ToList();
                return viagemViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public ViagemViewModelDetails GetById(int id)
        {
            try
            {
                Viagem viagem = _ViagemRepository.GetById(id);
                if (viagem == null) throw new Exception("Viagem não existe");
                ViagemViewModelDetails viagemModel = ViagemViewModelDetails.FromEntity(viagem);
                return viagemModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public void Update(int id, ViagemUpdateInputModel viagemModel)
        {
            try
            {
                Viagem viagemUpdated = viagemModel.ToEntity();
                Viagem viagem = _ViagemRepository.GetById(id);
                if (viagem is null) throw new Exception("Viagem não encontrada!");
                if (viagem.CTe != null) throw new Exception("Viagem não pode ser Atualizada!");
                viagem.Update(viagemUpdated);
                _ViagemRepository.Update(viagem);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Viagem viagem = _ViagemRepository.GetById(id);
                if (viagem is null) throw new Exception("Viagem não encontrada!");

                _ViagemRepository.Delete(viagem);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public Viagem CalculaValorFrete(int id)
        {
            try
            {
                Viagem viagem = _ViagemRepository.GetById(id);
                viagem.CalculaValorFrete();
                _ViagemRepository.Update(viagem);
                if (viagem.CTe != null)
                {
                    _CTeService.CalculaValorBasePorDentro(viagem.CTe.Id);
                }
                return viagem;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
        
    }
}
