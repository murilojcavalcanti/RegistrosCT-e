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
        private readonly IViagemService _ViagemService;

        public CargaService(ICargaRepository repository)
        {
            _Repository = repository;
        }

        public CargaViewModel Post(CargaInputModel cargaModel)
        {
            try
            {
                Carga carga = cargaModel.ToEntity();
                Carga cargaCreated = _Repository.Post(carga);
                return CargaViewModel.FromEntity(cargaCreated);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public List<CargaViewModel> GetAll()
        {
            try
            {
                List<Carga> cargas = _Repository.GetAll();
                List<CargaViewModel> cargasModel = cargas.Select(c => CargaViewModel.FromEntity(c)).ToList();
                return cargasModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public CargaViewModelDetails GetById(int id)
        {
            try
            {
                Carga carga = _Repository.GetById(id);
                CargaViewModelDetails cargaModel = CargaViewModelDetails.FromEntity(carga);
                return cargaModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public void Update(int id, CargaInputModel cargaModel)
        {
            try
            {
                Carga cargaUpdated = cargaModel.ToEntity();
                Carga carga = _Repository.GetById(id);
                if (carga is null) throw new Exception("Carga não encontrada!");
                if (carga.Viagem != null) throw new Exception("Carga não pode ser atualizada!");
                carga.Update(cargaUpdated);
                _Repository.Update(cargaUpdated);
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
                Carga carga = _Repository.GetById(id);
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
