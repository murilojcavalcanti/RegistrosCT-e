using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Infra.Repostories.CargaRepositories
{
    public interface ICargaRepository
    {
        Task<Carga> Post(Carga carga);
        Task<List<Carga>> GetAll();
        Task<Carga> GetById(int id);
        
        void Update(Carga carga);

        void Delete(Carga carga);
    }
}
