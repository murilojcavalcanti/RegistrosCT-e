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
        public CTeViewModel(decimal valorCTe, decimal iCMS, DateTime dataEmissao, int cTeId, decimal valorFrete)
        {
            ValorCTe = valorCTe;
            ICMS = iCMS;
            DataEmissao = dataEmissao;
            CTeId = cTeId;
            ValorFrete = valorFrete;
        }

        public int CTeId { get; set; }
        public decimal ValorCTe { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ICMS { get; set; }
        public DateTime DataEmissao { get; set; }

        public static CTeViewModel FromEntity(decimal valorCTe,  decimal valorICMS, DateTime dataEmissao, int Id, decimal valorFrete)
            => new(valorCTe,valorICMS,dataEmissao,Id,valorFrete);
    }
}
