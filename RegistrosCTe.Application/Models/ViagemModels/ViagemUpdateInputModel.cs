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
    public class ViagemUpdateInputModel
    {
        public ViagemUpdateInputModel(string origem, string destino, decimal distancia, DateTime dataInicio)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            DataInicio = dataInicio;
        }


        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(10, ErrorMessage = "O tamanho minimo de 10 caracteres")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(10, ErrorMessage = "O tamanho minimo de 10 caracteres")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Distancia { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }

        public Viagem ToEntity() => new(Origem,Destino,Distancia,DataInicio);
    }
}
