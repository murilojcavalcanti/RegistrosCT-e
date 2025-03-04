using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Models.CTeModels
{
    public class CTeViewModelDetails
    {
        public CTeViewModelDetails(decimal valorCTe, decimal iCMS, DateTime dataEmissao, Viagem viagem, int cTeId)
        {
            ValorCTe = valorCTe;
            ICMS = iCMS;
            DataEmissao = dataEmissao;
            Viagem = viagem !=null? ViagemViewModelDetails.FromEntity(viagem) : null;
            CTeId = cTeId;
        }

        public int CTeId { get; set; }
        public decimal ValorCTe { get; set; }
        public decimal ICMS { get; set; }
        public DateTime DataEmissao { get; set; }

        public ViagemViewModelDetails Viagem { get; set; }
        public static CTeViewModelDetails FromEntity(CTe CTe)
            => new(CTe.ValorCTe, CTe.ValorICMS, CTe.DataEmissao,CTe.Viagem,CTe.Id);
    }
}
