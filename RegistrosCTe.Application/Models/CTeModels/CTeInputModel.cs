using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Models.CTeModels
{
    public class CTeInputModel
    {
        public CTeInputModel(decimal valorCTe, decimal iCMS, int viagemId)
        {
            ValorCTe = valorCTe;
            ValorICMS = iCMS;
            ViagemId = viagemId;
        }

        public decimal ValorCTe { get; set; }
        public decimal ValorICMS { get; set; }
        public int ViagemId { get; set; }
        public CTe ToEntity() => new CTe(ValorCTe,ValorICMS,ViagemId);

    }
}
