using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Application.Services.ViagemService
{
    public interface IViagemService
    {
        ViagemViewModel Post(ViagemInputModel model);

        List<ViagemViewModel> GetAll();

        ViagemViewModelDetails GetById(int id); 

        void Update(int id, ViagemUpdateInputModel viagemModel);

        void Delete(int id);

        Viagem CalculaValorFrete(int id);
    }
}
