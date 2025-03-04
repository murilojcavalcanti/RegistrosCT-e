using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.Application.Services.ViagemService
{
    public interface IViagemService
    {
        Viagem Post(ViagemInputModel model);

        List<ViagemViewModel> GetAll();

        ViagemViewModelDetails GetById(int id); 

        void Update(int id, ViagemUpdateInputModel viagemModel);

        void Delete(int id);
    }
}
