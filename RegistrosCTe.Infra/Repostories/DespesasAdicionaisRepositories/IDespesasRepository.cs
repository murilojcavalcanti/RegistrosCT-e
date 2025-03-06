using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Infra.Repostories.DespesasAdicionaisRepositories
{
    public interface IDespesasRepository
    {
        Task<DespesaAdicional> Post(DespesaAdicional Despesa);
        Task<List<DespesaAdicional>> GetAll();
        Task<DespesaAdicional> GetById(int id);
        void Update(DespesaAdicional Despesa);
        void Delete(DespesaAdicional Despesa);
    }
}
