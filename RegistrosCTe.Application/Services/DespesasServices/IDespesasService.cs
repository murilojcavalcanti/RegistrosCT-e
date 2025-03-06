using RegistrosCTe.Domain.Entities;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;

namespace RegistrosCTe.Application.Services.DespesasServices
{
    public interface IDespesasService
    {
        Task<DespesaAdicionalViewModel> Post(DespesaAdicionalInputModel DespesaModel);
        Task<List<DespesaAdicionalViewModel>> GetAll();
        Task<DespesaAdicionalViewModelDetails> GetByIdAsync(int id);
        void Update(int id, DespesaAdicionalUpdateInputModel DespesaModel);
        void Delete(int id);
    }
}
