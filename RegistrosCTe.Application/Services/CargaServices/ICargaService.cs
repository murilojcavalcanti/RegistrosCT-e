using Microsoft.EntityFrameworkCore;
using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;
using RegistrosCTe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Application.Services.CargaServices
{
    public interface ICargaService
    {
        Carga Post(CargaInputModel cargaModel);
        List<CargaViewModel> GetAll();

        CargaViewModelDetails GetById(int id);

        void Update(int id, CargaInputModel cargaModel);

        void Delete(int id);
    }
}
