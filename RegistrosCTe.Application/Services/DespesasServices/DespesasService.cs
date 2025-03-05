using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;

namespace RegistrosCTe.Application.Services.DespesasServices
{
    public class DespesasService: IDespesasService
    {
        private readonly AppDbContext _context;

        public DespesasService(AppDbContext context)
        {
            _context = context;
        }

        public DespesaAdicional Post(DespesaAdicionalInputModel DespesaModel)
        {
            DespesaAdicional despesa = DespesaModel.ToEntity();
            _context.Set<DespesaAdicional>().Add(despesa);
            _context.SaveChanges();

            Viagem viagem = _context.Set<Viagem>().Include(c => c.Carga)
                .Include(v => v.DespesaAdicionais)
                .SingleOrDefault(v => v.Id == DespesaModel.ViagemId);
            viagem.CalculaValorFrete();
            viagem.Update(viagem);
            _context.SaveChanges();
            
            return despesa;
        }

        public List<DespesaAdicionalViewModel> GetAll()
        {
            List<DespesaAdicional> despesas = _context.Set<DespesaAdicional>().ToList();
            List<DespesaAdicionalViewModel> despesasModel = despesas.Select(v => DespesaAdicionalViewModel.FromEntity(v)).ToList();
            return despesasModel;
        }

        public DespesaAdicionalViewModelDetails GetById(int id)
        {
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().Include(d => d.Viagem).SingleOrDefault(v => v.Id == id);
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            DespesaAdicionalViewModelDetails despesaModel = DespesaAdicionalViewModelDetails.FromEntity(despesa);
            return despesaModel;
        }

        public void Update(int id, DespesaAdicionalUpdateInputModel despesaModel)
        {
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().Include(d=>d.Viagem).ThenInclude(v=>v.CTe).SingleOrDefault(v => v.Id == id);
            DespesaAdicional despesaUpdated = despesaModel.ToEntity(despesa.ViagemId);
            if(despesa is null) throw new Exception("Despesa não encontrada!");
            if(despesa.Viagem.CTe != null ) throw new Exception("Despesa não pode ser atualizada!");
            
            bool isEqual = despesa.Valor == despesaModel.Valor;
            
            despesa.Update(despesaUpdated);
            _context.SaveChanges();

            Viagem viagem = _context.Set<Viagem>().Include(v => v.DespesaAdicionais).SingleOrDefault(v => v.Id == despesa.ViagemId);
            if (!isEqual)
            {
                viagem.CalculaValorFrete();
                viagem.Update(viagem);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().Include(d=>d.Viagem).ThenInclude(v=>v.CTe).SingleOrDefault(v => v.Id == id);
            if(despesa.Viagem.CTe!=null) throw new Exception("Despesa não pode ser excluida encontrada! Deve-se excluir o CT-e Primeiro!");            
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            _context.Update(despesa);
            if(despesa.Viagem != null)
            {
                Viagem viagem = despesa.Viagem;
                viagem.RecalculaValorFrete(despesa.Valor);
                viagem.Update(viagem);
            }
            _context.SaveChanges();

        }
    }
}
