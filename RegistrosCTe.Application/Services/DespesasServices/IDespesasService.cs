using RegistrosCTe.Domain.Entities;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;

namespace RegistrosCTe.Application.Services.DespesasServices
{
    public interface IDespesasService
    {
        DespesaAdicional Post(DespesaAdicionalInputModel DespesaModel);
        List<DespesaAdicionalViewModel> GetAll();
        DespesaAdicionalViewModelDetails GetById(int id);
        void Update(int id, DespesaAdicionalUpdateInputModel DespesaModel);
        void Delete(int id);
    }
}
