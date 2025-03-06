using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Infra.Repostories.CargaRepositories
{
    public interface ICargaRepository
    {
        Carga Post(Carga carga);
        List<Carga> GetAll();

        Carga GetById(int id);

        void Update(int id, Carga cargaModel);

        void Delete(int id);
    }
}
