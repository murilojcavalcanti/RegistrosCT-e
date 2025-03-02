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
        public CargaViewModel(int quantidade, decimal peso, decimal volume)
        {
            Quantidade = quantidade;
            Peso = peso;
            Volume = volume;
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

        public static CargaViewModel FromEntity(Carga carga)
            => new(carga.Quantidade,carga.Peso,carga.Volume);
    }
}
