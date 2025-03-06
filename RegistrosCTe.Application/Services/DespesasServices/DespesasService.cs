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

        public async Task<DespesaAdicionalViewModel> Post(DespesaAdicionalInputModel despesaModel)
        {
            DespesaAdicional despesa = despesaModel.ToEntity();
            try
            {
                DespesaAdicionalViewModel despesaCreated = DespesaAdicionalViewModel.FromEntity(await _Repository.Post(despesa));
                _ViagemService.CalculaValorFrete(despesa.ViagemId);
                return despesaCreated;
            }
            catch (Exception ex)
            {
                if (despesa.Id > 0) Delete(despesa.Id);
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task<List<DespesaAdicionalViewModel>> GetAll()
        {
            try
            {
                List<DespesaAdicional> despesas = await _Repository.GetAll();
                List<DespesaAdicionalViewModel> despesasModel = despesas.Select(v => DespesaAdicionalViewModel.FromEntity(v)).ToList();
                return despesasModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task<DespesaAdicionalViewModelDetails> GetById(int id)
        {
            try
            {
                DespesaAdicional despesa = await _Repository.GetById(id);
                if (despesa is null) throw new Exception("Despesa não encontrada!");
                DespesaAdicionalViewModelDetails despesaModel = DespesaAdicionalViewModelDetails.FromEntity(despesa);
                return despesaModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task Update(int id, DespesaAdicionalUpdateInputModel despesaModel)
        {
            try
            {
                DespesaAdicional despesa = await  _Repository.GetById(id);
                DespesaAdicional despesaUpdated = despesaModel.ToEntity();
                if (despesa is null) throw new Exception("Despesa não encontrada!");
                if (despesa.Viagem.CTe != null) throw new Exception("Despesa não pode ser atualizada!");
                despesa.Update(despesaUpdated);
                _Repository.Update(despesa);
                _ViagemService.CalculaValorFrete(despesa.ViagemId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                DespesaAdicional despesa = await _Repository.GetById(id);
                if (despesa is null) throw new Exception("Despesa não encontrada!");
                if (despesa.Viagem.CTe != null) throw new Exception("Despesa não pode ser atualizada!");
                _Repository.Delete(despesa);
                _ViagemService.CalculaValorFrete(despesa.ViagemId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
    }
}
