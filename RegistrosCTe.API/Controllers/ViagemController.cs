using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ViagemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ViagemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Viagem viagem)
        {
            await _context.Set<Viagem>().AddAsync(viagem);
            _context.SaveChanges();
            return Created();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Viagem> viagens = await _context.Set<Viagem>().ToListAsync();
            return Ok(viagens);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Viagem viagem = await _context.Set<Viagem>().SingleOrDefaultAsync(v => v.Id == id);
            return Ok(viagem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,Viagem viagemUpdated)
        {
            Viagem viagem = await _context.Set<Viagem>().SingleOrDefaultAsync(v => v.Id == id);
            if (viagem is null) return BadRequest("Viagem não encontrada!");
            viagem.Update(viagemUpdated);
            _context.SaveChanges();
            return Ok();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Viagem viagem = await _context.Set<Viagem>().SingleOrDefaultAsync(v => v.Id == id);
            if (viagem is null) return BadRequest("Viagem não encontrada!");
            viagem.SetAsDeleted();
            viagem.Update(viagem);
            _context.SaveChanges();
            return Ok();
        }

    }
}
