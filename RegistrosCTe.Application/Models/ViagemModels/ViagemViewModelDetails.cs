using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CTeModels;
using RegistrosCTe.Domain.Entities;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Models.ViagemModels
{
    public class ViagemViewModelDetails
    {
        public ViagemViewModelDetails(string origem, string destino, decimal distancia, DateTime dataInicio, decimal valorFrete,
            Carga carga, List<DespesaAdicional> despesaAdicionais, int viagemId)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            DataInicio = dataInicio;
            ValorFrete = valorFrete;
            Carga = carga != null ? CargaViewModel.FromEntity(carga) : null;
            DespesaAdicionais = despesaAdicionais != null ? despesaAdicionais.Select(d => DespesaAdicionalViewModel.FromEntity(d)).ToList() : null;
            ViagemId = viagemId;
        }

        public int ViagemId { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Distancia { get; set; }
        public DateTime DataInicio { get; set; }
        public decimal ValorFrete { get; set; }
        public virtual CargaViewModel Carga { get; set; }
        public List<DespesaAdicionalViewModel> DespesaAdicionais { get; set; }

        public static ViagemViewModelDetails FromEntity(Viagem viagem)
            => new(viagem.Origem,viagem.Destino,viagem.Distancia,viagem.DataInicio,viagem.ValorFrete,viagem.Carga,viagem.DespesaAdicionais,viagem.Id);
    }
}
