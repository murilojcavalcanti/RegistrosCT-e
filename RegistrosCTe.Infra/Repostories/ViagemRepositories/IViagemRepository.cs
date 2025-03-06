using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Infra.Repostories.ViagemRepositories
{
    public interface IViagemRepository
    {
        Task<Viagem> Post(Viagem model);
        Task<List<Viagem>> GetAll();

        Viagem GetById(int id);

        void Update(Viagem viagem);
        
        void Delete(Viagem viagem);
    }
}
