using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Models.CTeModels
{
    public class CTeViewModel
    {
        public CTeViewModel(decimal valorCTe, decimal iCMS, DateTime dataEmissao)
        {
            ValorCTe = valorCTe;
            ICMS = iCMS;
            DataEmissao = dataEmissao;
        }

        public decimal ValorCTe { get; set; }
        public decimal ICMS { get; set; }
        public DateTime DataEmissao { get; set; }

        public static CTeViewModel FromEntity(CTe CTe)
            => new(CTe.ValorCTe,CTe.ValorICMS,CTe.DataEmissao);
    }
}
