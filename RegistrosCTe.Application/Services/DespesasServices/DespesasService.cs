using RegistrosCTe.Application.Services.ViagemService;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.DespesasAdicionaisRepositories;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;

namespace RegistrosCTe.Application.Services.DespesasServices
{
    public class DespesasService: IDespesasService
    {
        private readonly IDespesasRepository _Repository;
        private readonly IViagemService _ViagemService;
        public DespesasService(IDespesasRepository repository, IViagemService viagemService)
        {
            _Repository = repository;
            _ViagemService = viagemService;
        }

        public DespesaAdicionalViewModel Post(DespesaAdicionalInputModel despesaModel)
        {
            DespesaAdicional despesa = despesaModel.ToEntity();
            DespesaAdicionalViewModel despesaCreated = DespesaAdicionalViewModel.FromEntity(_Repository.Post(despesa));
            _ViagemService.CalculaValorFrete(despesa.ViagemId);
            return despesaCreated;
        }

        public List<DespesaAdicionalViewModel> GetAll()
        {
            List<DespesaAdicional> despesas = _Repository.GetAll();
            List<DespesaAdicionalViewModel> despesasModel = despesas.Select(v => DespesaAdicionalViewModel.FromEntity(v)).ToList();
            return despesasModel;
        }

        public DespesaAdicionalViewModelDetails GetById(int id)
        {
            DespesaAdicional despesa = _Repository.GetById(id);
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            DespesaAdicionalViewModelDetails despesaModel = DespesaAdicionalViewModelDetails.FromEntity(despesa);
            return despesaModel;
        }

        public void Update(int id, DespesaAdicionalUpdateInputModel despesaModel)
        {
            DespesaAdicional despesa = _Repository.GetById(id);
            DespesaAdicional despesaUpdated = despesaModel.ToEntity();
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            if (despesa.Viagem.CTe != null) throw new Exception("Despesa não pode ser atualizada!");
            despesa.Update(despesaUpdated);
            _Repository.Update(despesa);
            _ViagemService.CalculaValorFrete(despesa.ViagemId);
        }

        public void Delete(int id)
        {
            DespesaAdicional despesa = _Repository.GetById(id);
            _Repository.Delete(despesa);
            _ViagemService.RecalculaValorFrete(despesa.ViagemId,despesa.Valor);
        }
    }
}
