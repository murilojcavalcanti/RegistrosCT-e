using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.CTeModels;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.CTeRepositories;

namespace RegistrosCTe.Application.Services.CTeService
{
    public class CTeService:ICTeService
    {
        private readonly AppDbContext _context;
        private readonly ICTeRepository _Repository;

        public CTeService(ICTeRepository repository)
        {
            _Repository = repository;
        }
        public CTeViewModel Post(CTeInputModel cteModel)
        {
            try
            {
                CTe cte = cteModel.ToEntity();
                CTe cteCreated = _Repository.Post(cte);
                CTeViewModel cteCalculado = CalculaValorBasePorDentro(cteCreated.Id);
                return cteCalculado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
        public List<CTeViewModel> GetAll()
        {
            try
            {
                var ctes = _Repository.GetAll().Select(c => new { c.Id, c.ValorCTe, c.ValorICMS, valorFrete = c.Viagem.ValorFrete, c.DataEmissao });
                List<CTeViewModel> cteModels = ctes.Select(c => CTeViewModel.FromEntity(c.ValorCTe, c.ValorICMS, c.DataEmissao, c.Id, c.valorFrete)).ToList();
                return cteModels;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
        public CTeViewModelDetails GetById(int id)
        {
            try
            {
                CTe cte = _Repository.GetById(id);
                CTeViewModelDetails cteModel = CTeViewModelDetails.FromEntity(cte);
                return cteModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
        public void Delete(int id)
        {
            try
            {
                CTe cte = _Repository.GetById(id);
                _Repository.Delete(cte);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
        public CTeViewModel CalculaValorBaseSimples(int id)
        {
            try
            {
                CTe cte = _Repository.GetById(id);
                cte.CalculaValorBaseSimples();
                _Repository.Update(cte);
                return CTeViewModel.FromEntity(cte.ValorCTe, cte.ValorICMS, cte.DataEmissao, cte.Id, cte.Viagem.ValorFrete);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
        public CTeViewModel CalculaValorBasePorDentro(int id)
        {
            try
            {
                CTe cte = _Repository.GetById(id);
                cte.CalculaValorBasePorDentro();
                _Repository.Update(cte);
                return CTeViewModel.FromEntity(cte.ValorCTe, cte.ValorICMS, cte.DataEmissao, cte.Id, cte.Viagem.ValorFrete);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no processamento:{ex.Message}!");
            }
        }
    }
}
