using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;
using System.Text.Json;

namespace RegistrosCTe.Infra.Repostories.ViagemRepositories
{
    public class ViagemRepository:IViagemRepository
    {
        private readonly AppDbContext _context; 
        private readonly IDistributedCache _cache;
        public ViagemRepository(AppDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Viagem> Post(Viagem viagem)
        {
            _context.Set<Viagem>().Add(viagem);
            _context.SaveChanges();
            await _cache.RemoveAsync("viagens");
            return viagem;
        }

        public async Task<List<Viagem>> GetAll()
        {

            try
            {
                var cacheviagens = await _cache.GetStringAsync("viagens");
                if (!string.IsNullOrEmpty(cacheviagens))
                {
                    return JsonSerializer.Deserialize<List<Viagem>>(cacheviagens);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao consultar o cache");
            }

            List<Viagem> viagens = _context.Set<Viagem>().ToList();
            if (viagens == null) throw new Exception("Viagens não existem");

            try
            {
                var opts = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await _cache.SetAsync("viagens", JsonSerializer.SerializeToUtf8Bytes(viagens), opts);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar o cache");
            }
            return viagens;

        }

        public Viagem GetById(int id)
        {
            Viagem viagem = _context.Set<Viagem>()
                .Include(v => v.CTe)
                .Include(v => v.Carga).Include(v => v.DespesaAdicionais)
                .SingleOrDefault(v => v.Id == id);
            if (viagem == null) throw new Exception("Viagem não existe");
            return viagem;
        }

        public async void Update(Viagem viagemUpdated)
        {
            _context.Update(viagemUpdated);
            _context.SaveChanges();
            await _cache.RemoveAsync("viagens");
        }

        public async void Delete(Viagem viagem)
        {   
            _context.Remove(viagem);
            _context.SaveChanges();
            await _cache.RemoveAsync("viagens");

        }
    }
}
