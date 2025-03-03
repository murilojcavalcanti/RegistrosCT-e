using Microsoft.EntityFrameworkCore;
using RegistrosCTe.Application.Models.CTeModels;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Services.CTeService
{
    public interface ICTeService
    {
        CTe Post(CTeInputModel cteModel);
        List<CTeViewModel> GetAll();
        CTeViewModelDetails GetById(int id);
        void Delete(int id);
    }
}
