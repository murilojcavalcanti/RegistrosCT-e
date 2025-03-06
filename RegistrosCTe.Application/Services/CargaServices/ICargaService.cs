using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;

namespace RegistrosCTe.Application.Services.CargaServices
{
    public interface ICargaService
    {
        Task<CargaViewModel> Post(CargaInputModel cargaModel);
        Task<List<CargaViewModel>> GetAllAsync();
        Task<CargaViewModelDetails> GetByIdAsync(int id);

        void UpdateAsync(int id, CargaInputModel cargaModel);

        void DeleteAsync(int id);
    }
}
