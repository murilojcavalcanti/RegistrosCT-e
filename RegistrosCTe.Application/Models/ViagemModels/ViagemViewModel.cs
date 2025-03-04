using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Domain.Entities;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Models.ViagemModels
{
    public class ViagemViewModel
    {
        public ViagemViewModel(string origem, string destino, decimal distancia, DateTime dataInicio, decimal valorFrete, int viagemId)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            DataInicio = dataInicio;
            ValorFrete = valorFrete;
            ViagemId = viagemId;
        }

        public int ViagemId { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Distancia { get; set; }
        public DateTime DataInicio { get; set; }
        public decimal ValorFrete { get; set; }
        
        public static ViagemViewModel FromEntity(Viagem viagem)
            => new(viagem.Origem, viagem.Destino, viagem.Distancia, viagem.DataInicio, viagem.ValorFrete,viagem.Id);
    }
}
