using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels
{
    public class DespesaAdicionalViewModel
    {
        public DespesaAdicionalViewModel(string nome, string description, decimal valor, int despesasId)
        {
            Nome = nome;
            Description = description;
            Valor = valor;
            DespesasId = despesasId;
        }

        public int DespesasId { get; set; }
        public string Nome { get; set; }
        public string Description { get; set; }
        public decimal Valor { get; set; }
        public static DespesaAdicionalViewModel FromEntity(DespesaAdicional DespesaAdicional)
            => new(DespesaAdicional.Nome, DespesaAdicional.Descricao, DespesaAdicional.Valor,DespesaAdicional.Id);

    }
}
