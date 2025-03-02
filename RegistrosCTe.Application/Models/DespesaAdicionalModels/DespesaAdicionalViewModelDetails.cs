using RegistrosCTe.Application.Models.CTeModels;
using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels
{
    public class DespesaAdicionalViewModelDetails
    {
        public DespesaAdicionalViewModelDetails(string nome, string description, decimal valor, Viagem viagem)
        {
            Nome = nome;
            Description = description;
            Valor = valor;
            Viagem = ViagemViewModel.FromEntity(viagem);
        }

        public string Nome { get; set; }
        public string Description { get; set; }
        public decimal Valor { get; set; }
        public virtual ViagemViewModel Viagem { get; set; }
        public static DespesaAdicionalViewModelDetails FromEntity(DespesaAdicional DespesaAdicional)
            => new(DespesaAdicional.Nome,DespesaAdicional.Descricao,DespesaAdicional.Valor,DespesaAdicional.Viagem);
    }
}
