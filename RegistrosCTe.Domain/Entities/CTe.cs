using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Domain.Entities
{
    public class CTe:BaseEntity
    {
        public CTe(decimal valorCTe, decimal iCMS, int viagemId)
        {
            ValorCTe = valorCTe;
            ValorICMS = iCMS;
            ViagemId = viagemId;
            DataEmissao = DateTime.Now;
        }

        public decimal ValorCTe { get; set; }
        public decimal ValorICMS { get; set; }
        public DateTime DataEmissao { get; set; }
        public int ViagemId { get; set; }
        public Viagem Viagem { get; set; }

        public void CalculaValorBaseSimples(decimal aliquota)
        {
            decimal PortentagemAliquota = (aliquota / 100);
            decimal baseCalculo = Viagem.ValorFrete;
            ValorICMS = baseCalculo * PortentagemAliquota;
            ValorCTe = baseCalculo + ValorICMS;
        }

        public void CalculaValorBasePorDentro(decimal aliquota)
        {
            decimal PortentagemAliquota = (aliquota / 100);
            decimal baseCalculo = Viagem.ValorFrete / (1 - PortentagemAliquota);
            ValorICMS = baseCalculo * PortentagemAliquota;
            ValorCTe = baseCalculo;
        }
    }
}
