
using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;
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

        public CargaViewModel Post(CargaInputModel cargaModel)
        {
            Carga carga = cargaModel.ToEntity();
            Carga cargaCreated = _Repository.Post(carga);
            return CargaViewModel.FromEntity(cargaCreated);
        }

        public List<CargaViewModel> GetAll()
        {
            List<Carga> cargas = _Repository.GetAll();
            List<CargaViewModel> cargasModel = cargas.Select(c => CargaViewModel.FromEntity(c)).ToList();
            return cargasModel;
        }

        public CargaViewModelDetails GetById(int id)
        {
            Carga carga = _Repository.GetById(id);
            CargaViewModelDetails cargaModel = CargaViewModelDetails.FromEntity(carga);
            return cargaModel;
        }

        public void Update(int id, CargaInputModel cargaModel)
        {
            Carga cargaUpdated = cargaModel.ToEntity();
            _Repository.Update(id, cargaUpdated);
        }

        
        public void Delete(int id)
        {
            _Repository.Delete(id);
        }

    }
}
