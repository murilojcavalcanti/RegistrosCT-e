using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CargaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CargaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Carga carga)
        {
            await _context.Set<Carga>().AddAsync(carga);
            _context.SaveChanges();
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Carga> cargas = await _context.Set<Carga>().ToListAsync();
            return Ok(cargas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Carga carga = await _context.Set<Carga>().SingleOrDefaultAsync(v => v.Id == id);
            return Ok(carga);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Carga CargaUpdated)
        {
            Carga Carga = await _context.Set<Carga>().SingleOrDefaultAsync(v => v.Id == id);
            if (Carga is null) return BadRequest("Carga não encontrada!");
            Carga.Update(CargaUpdated);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Carga Carga = await _context.Set<Carga>().SingleOrDefaultAsync(v => v.Id == id);
            if (Carga is null) return BadRequest("Carga não encontrada!");
            Carga.SetAsDeleted();
            Carga.Update(Carga);
            _context.SaveChanges();
            return Ok();
        }
    }
}
