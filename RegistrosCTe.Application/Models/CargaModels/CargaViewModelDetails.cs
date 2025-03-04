using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Models.CargaModels
{
    public class CargaViewModelDetails
    {
        public CargaViewModelDetails(int quantidade, decimal peso, decimal volume, Viagem viagem, int cargaId)
        {
            Quantidade = quantidade;
            Peso = peso;
            Volume = volume;
            Viagem = viagem != null ? ViagemViewModel.FromEntity(viagem) : null;
            CargaId = cargaId;
        }

        public int CargaId { get; set; }
        public int Quantidade { get; set; }
        public decimal Peso { get; set; }
        public decimal Volume { get; set; }
        public virtual ViagemViewModel Viagem { get; set; }
        public static CargaViewModelDetails FromEntity(Carga carga)
                => new(carga.Quantidade, carga.Peso, carga.Volume, carga.Viagem, carga.Id);
    }
}
