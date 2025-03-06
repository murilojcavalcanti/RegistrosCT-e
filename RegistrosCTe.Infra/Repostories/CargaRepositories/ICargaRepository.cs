using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Infra.Repostories.CargaRepositories
{
    public interface ICargaRepository
    {
        Carga Post(Carga carga);
        List<Carga> GetAll();

        Carga GetById(int id);
        
        void Update(Carga carga);

        void Delete(Carga carga);
    }
}
