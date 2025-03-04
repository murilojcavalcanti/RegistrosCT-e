using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Application.Models.CargaModels
{
    public class CargaViewModel
    {
        public CargaViewModel(int quantidade, decimal peso, decimal volume, int cargaId)
        {
            CargaId = cargaId;
            Quantidade = quantidade;
            Peso = peso;
            Volume = volume;
        }

        public int CargaId { get; set; }
        public int Quantidade { get; set; }

        public decimal Peso { get; set; }

        public decimal Volume { get; set; }

        public static CargaViewModel FromEntity(Carga carga)
            => new(carga.Quantidade,carga.Peso,carga.Volume,carga.Id);
    }
}
