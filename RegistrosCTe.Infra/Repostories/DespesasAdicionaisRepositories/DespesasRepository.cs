using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;
using System.Text.Json;

namespace RegistrosCTe.Infra.Repostories.DespesasAdicionaisRepositories
{
    public class DespesasRepository:IDespesasRepository
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;


        public DespesasRepository(AppDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<DespesaAdicional> Post(DespesaAdicional despesa)
        {
            _context.Set<DespesaAdicional>().Add(despesa);
            _context.SaveChanges();
            await _cache.RemoveAsync("despesas");
            return despesa;
        }

        public async Task<List<DespesaAdicional>> GetAll()
        {

            try
            {
                var cacheDespesa = await _cache.GetStringAsync("despesas");
                if (!string.IsNullOrEmpty(cacheDespesa))
                {
                    return JsonSerializer.Deserialize<List<DespesaAdicional>>(cacheDespesa);
                }
            }catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao consultar o cache");
            }
            List<DespesaAdicional> despesas = await _context.Set<DespesaAdicional>().ToListAsync();
            try
            {
                var opts = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await _cache.SetAsync("despesas", JsonSerializer.SerializeToUtf8Bytes(despesas), opts);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cache");
            }
            return despesas;
        }

        public async Task<DespesaAdicional> GetById(int id)
        { 
            DespesaAdicional despesa = _context.Set<DespesaAdicional>().Include(d => d.Viagem).ThenInclude(v=>v.CTe).SingleOrDefault(v => v.Id == id);
            if (despesa is null) throw new Exception("Despesa não encontrada!");
            return despesa;
        }

        public async void Update(DespesaAdicional despesaUpdated)
        {
            _context.Update(despesaUpdated);
            _context.SaveChanges();
            await _cache.RemoveAsync("despesas");
        }

        public async void Delete(DespesaAdicional despesa)
        {
            _context.Remove(despesa);
            _context.SaveChanges();
            await _cache.RemoveAsync("despesas");
        }
    }
}
