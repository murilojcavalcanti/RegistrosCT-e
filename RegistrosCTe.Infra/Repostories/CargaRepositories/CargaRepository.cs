using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;
using System.Text.Json;

namespace RegistrosCTe.Infra.Repostories.CargaRepositories
{
    public class CargaRepository:ICargaRepository
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;

        public CargaRepository(AppDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Carga> Post(Carga carga)
        {
            _context.Set<Carga>().Add(carga);
            _context.SaveChanges();
            await _cache.RemoveAsync("cargas");
            
            return carga;
        }

        public async Task<List<Carga>> GetAll()
        {
            List<Carga> cargas =_context.Set<Carga>().AsNoTracking().ToList();
            if (cargas is null) throw new Exception("Cargas não encontradas");
            return cargas;
        }

        public async Task<Carga> GetById(int id)
        {
            var cacheCargaKey = $"carga_{id}";
            var cacheCarga = await _cache.GetStringAsync(cacheCargaKey);

            if (!string.IsNullOrEmpty(cacheCarga))
            {
                return JsonSerializer.Deserialize<Carga>(cacheCarga);
            }
            
            Carga carga = _context.Set<Carga>().Include(c => c.Viagem).SingleOrDefault(v => v.Id == id);
            if (carga is null) throw new Exception("Carga não encontrada!");

            var opts = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(300)
            };
            await _cache.SetStringAsync(cacheCargaKey, JsonSerializer.Serialize(carga), opts);
            return carga;
        }
        public async void Update(Carga cargaUpdated)
        {
            _context.Update(cargaUpdated);
            _context.SaveChanges();
            await _cache.RemoveAsync("despesas");
            await _cache.RemoveAsync($"carga_{cargaUpdated.Id}");
        }


        public async void Delete(Carga carga)
        {
            _context.Remove(carga);
            _context.SaveChanges();
            await _cache.RemoveAsync("despesas");
            await _cache.RemoveAsync($"carga_{carga.Id}");
        }

    }
}
