using RegistrosCTe.Domain.Entities;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;

namespace RegistrosCTe.Application.Services.DespesasServices
{
    public interface IDespesasService
    {
        Task<DespesaAdicionalViewModel> Post(DespesaAdicionalInputModel DespesaModel);
        Task<List<DespesaAdicionalViewModel>> GetAll();
        Task<DespesaAdicionalViewModelDetails> GetById(int id);
        Task Update(int id, DespesaAdicionalUpdateInputModel DespesaModel);
        Task Delete(int id);
    }
}
