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
            List<DespesaAdicional> despesas = _context.Set<DespesaAdicional>().Where(d => d.IsDeleted).ToList();
            List<DespesaAdicionalViewModel> despesasModel = despesas.Select(v => DespesaAdicionalViewModel.FromEntity(v)).ToList();
            return despesasModel;
        }

        public DespesaAdicionalViewModelDetails GetById(int id)
        {
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().Where(d => d.IsDeleted).Include(d => d.Viagem).SingleOrDefault(v => v.Id == id);
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            DespesaAdicionalViewModelDetails despesaModel = DespesaAdicionalViewModelDetails.FromEntity(despesa);
            return despesaModel;
        }

        public void Update(int id, DespesaAdicionalUpdateInputModel DespesaModel)
        {
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().Where(d => d.IsDeleted).SingleOrDefault(v => v.Id == id);
            DespesaAdicional despesaUpdated = DespesaModel.ToEntity(despesa.ViagemId);
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            despesa.Update(despesaUpdated);
            _context.SaveChanges();

            Viagem viagem = _context.Set<Viagem>().Include(v => v.DespesaAdicionais).Where(d => d.IsDeleted).SingleOrDefault(v => v.Id == despesa.ViagemId);
            DespesaAdicional despesaViagem = viagem.DespesaAdicionais.FirstOrDefault(d => d.Id == despesa.Id);
            if (despesa.Valor != despesaViagem.Valor)
            {
                viagem.CalculaValorFrete();
                viagem.Update(viagem);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().SingleOrDefault(v => v.Id == id);
            if (despesa is null) throw new Exception("Despesa não encontrada!");

            despesa.SetAsDeleted();
            _context.Update(despesa);
            _context.SaveChanges();
        }
    }
}
