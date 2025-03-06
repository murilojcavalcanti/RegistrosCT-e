using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Infra.Repostories.CTeRepositories
{
    public interface ICTeRepository
    {
        CTe Post(CTe cte);
        List<CTe> GetAll();
        CTe GetById(int id);
        void Delete(CTe cte);
        CTe Update(CTe cteUpdated);
    }
}
