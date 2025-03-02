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
        public CTe(decimal valorCTe, decimal iCMS)
        {
            ValorCTe = valorCTe;
            ICMS = iCMS;
        }

        public decimal ValorCTe { get; set; }
        public decimal ICMS { get; set; }
        public int ViagemId { get; set; }
        public Viagem Viagem { get; set; }

        public void CalculaValorBaseSimples(decimal aliquota)
        {
            decimal PortentagemAliquota = (aliquota / 100);
            decimal baseCalculo = Viagem.ValorFrete;
            ICMS = baseCalculo * PortentagemAliquota;
            ValorCTe = baseCalculo + ICMS;
        }

        public void CalculaValorBasePorDentro(decimal aliquota)
        {
            decimal PortentagemAliquota = (aliquota / 100);
            decimal baseCalculo = Viagem.ValorFrete / (1 - PortentagemAliquota);
            ICMS = baseCalculo * PortentagemAliquota;
            ValorCTe = baseCalculo;
        }
    }
}
