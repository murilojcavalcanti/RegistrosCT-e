using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;

namespace RegistrosCTe.Application.Services.CargaServices
{
    public interface ICargaService
    {
        Task<CargaViewModel> Post(CargaInputModel cargaModel);
        Task<List<CargaViewModel>> GetAll();
        Task<CargaViewModelDetails> GetById(int id);

        Task Update(int id, CargaInputModel cargaModel);

        Task Delete(int id);
    }
}
