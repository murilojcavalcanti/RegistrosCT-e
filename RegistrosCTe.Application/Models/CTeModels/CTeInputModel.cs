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
        public CTeInputModel(decimal aliquota, int viagemId)
        {

            Aliquota = aliquota;
            ViagemId = viagemId;
        }

        public decimal Aliquota { get; set; }
        public int ViagemId { get; set; }
        public CTe ToEntity() => new CTe(Aliquota, ViagemId);

    }
}
