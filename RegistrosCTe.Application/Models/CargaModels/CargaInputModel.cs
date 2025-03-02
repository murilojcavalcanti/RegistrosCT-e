using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Models.CargaModelss
{
    public class CargaInputModel
    {
        public CargaInputModel(int quantidade, decimal peso, decimal volume)
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
        public Carga ToEntity() => new Carga(Quantidade,Peso,Volume);
        
    }
}
