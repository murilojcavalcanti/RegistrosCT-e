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
        public DespesaAdicionalViewModelDetails(string nome, string descricao, decimal valor, Viagem viagem, int despesasId)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Viagem = viagem!=null ? ViagemViewModel.FromEntity(viagem) : null;
            DespesasId = despesasId;
        }

        public int DespesasId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public virtual ViagemViewModel Viagem { get; set; }
        public static DespesaAdicionalViewModelDetails FromEntity(DespesaAdicional DespesaAdicional)
            => new(DespesaAdicional.Nome,DespesaAdicional.Descricao,DespesaAdicional.Valor,DespesaAdicional.Viagem,DespesaAdicional.Id);
    }
}
