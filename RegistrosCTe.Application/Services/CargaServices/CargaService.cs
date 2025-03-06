using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;
using RegistrosCTe.Application.Services.ViagemService;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.CargaRepositories;

namespace RegistrosCTe.Application.Services.CargaServices
{
    public class CargaService:ICargaService
    {
        private readonly ICargaRepository _Repository;

        public CargaService(ICargaRepository repository)
        {
            _Repository = repository;
        }

        public async Task<CargaViewModel> Post(CargaInputModel cargaModel)
        {
            try
            {
                Carga carga = cargaModel.ToEntity();
                Carga cargaCreated =await _Repository.Post(carga);
                return CargaViewModel.FromEntity(cargaCreated);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task<List<CargaViewModel>> GetAll()
        {
            try
            {
                List<Carga> cargas = await _Repository.GetAll();
                List<CargaViewModel> cargasModel = cargas.Select(c => CargaViewModel.FromEntity(c)).ToList();
                return cargasModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task<CargaViewModelDetails> GetById(int id)
        {
            try
            {
                Carga carga = await _Repository.GetById(id);
                CargaViewModelDetails cargaModel = CargaViewModelDetails.FromEntity(carga);
                return cargaModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task Update(int id, CargaInputModel cargaModel)
        {
            try
            {
                Carga cargaUpdated = cargaModel.ToEntity();
                Carga carga = await _Repository.GetById(id);
                if (carga is null) throw new Exception("Carga não encontrada!");
                if (carga.Viagem != null) throw new Exception("Carga não pode ser atualizada!");
                carga.Update(cargaUpdated);
                _Repository.Update(carga);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        
        public async Task Delete(int id)
        {
            try
            {
                Carga carga = await _Repository.GetById(id);
                if (carga is null) throw new Exception("Carga não encontrada!");
                if (carga?.Viagem != null) throw new Exception("Carga não pode ser Deletada!");
                _Repository.Delete(carga);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

    }
}
