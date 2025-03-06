using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Infra.Repostories.DespesasAdicionaisRepositories
{
    public interface IDespesasRepository
    {
        DespesaAdicional Post(DespesaAdicional Despesa);
        List<DespesaAdicional> GetAll();
        DespesaAdicional GetById(int id);
        void Update(DespesaAdicional Despesa);
        void Delete(DespesaAdicional Despesa);
    }
}
