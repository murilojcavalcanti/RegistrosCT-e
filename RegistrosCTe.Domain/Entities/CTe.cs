using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Domain.Entities
{
    public class CTe:BaseEntity
    {
        public CTe(decimal aliquota, int viagemId)
        {
            Aliquota = aliquota;
            ViagemId = viagemId;
            DataEmissao = DateTime.Now;
        }


        [Required(ErrorMessage = "campo Obrigatorio")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorCTe { get; set; }
        
        [Required(ErrorMessage = "campo Obrigatorio")]
        [Column(TypeName = "decimal(10,2)")]

        public decimal ValorICMS { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Aliquota { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "campo Obrigatorio")]
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        public int ViagemId { get; set; }
        public Viagem Viagem { get; set; }

        public void CalculaValorBaseSimples()
        {
            decimal PortentagemAliquota = (Aliquota / 100);
            decimal baseCalculo = Viagem.ValorFrete;
            ValorICMS = baseCalculo * PortentagemAliquota;
            ValorCTe = baseCalculo + ValorICMS;
        }

        public void CalculaValorBasePorDentro()
        {
            decimal PortentagemAliquota = (Aliquota / 100);
            decimal baseCalculo = Viagem.ValorFrete / (1 - PortentagemAliquota);
            ValorICMS = baseCalculo * PortentagemAliquota;
            ValorCTe = baseCalculo;
        }
        
    }
}
