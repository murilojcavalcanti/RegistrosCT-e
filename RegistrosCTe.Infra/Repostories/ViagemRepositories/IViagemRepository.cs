using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Infra.Repostories.ViagemRepositories
{
    public interface IViagemRepository
    {
        Viagem Post(Viagem model);

        List<Viagem> GetAll();

        Viagem GetById(int id);

        void Update(Viagem viagem);
        
        void Delete(Viagem viagem);
    }
}
