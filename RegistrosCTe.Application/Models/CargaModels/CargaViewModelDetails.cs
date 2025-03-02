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
        public CargaViewModelDetails(int quantidade, decimal peso, decimal volume, Viagem viagem)
        {
            Quantidade = quantidade;
            Peso = peso;
            Volume = volume;
            Viagem =ViagemViewModel.FromEntity(viagem);
        }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Volume { get; set; }
        public virtual ViagemViewModel Viagem { get; set; }
        public static CargaViewModelDetails FromEntity(Carga carga)
                => new(carga.Quantidade, carga.Peso, carga.Volume, carga.Viagem);
    }
}
