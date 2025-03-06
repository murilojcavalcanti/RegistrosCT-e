using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Infra.Repostories.ViagemRepositories
{
    public interface IViagemRepository
    {
        Task<Viagem> Post(Viagem model);
        Task<List<Viagem>> GetAll();

        Viagem GetById(int id);

        Task Update(Viagem viagem);
        
        Task Delete(Viagem viagem);
    }
}
