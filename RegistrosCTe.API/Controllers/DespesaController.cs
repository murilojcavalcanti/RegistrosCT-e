using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DespesaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(DespesaAdicional Despesa)
        {
            await _context.Set<DespesaAdicional>().AddAsync(Despesa);
            _context.SaveChanges();
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<DespesaAdicional> Despesas = await _context.Set<DespesaAdicional>().ToListAsync();
            return Ok(Despesas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            DespesaAdicional Despesa = await _context.Set<DespesaAdicional>().SingleOrDefaultAsync(v => v.Id == id);
            return Ok(Despesa);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, DespesaAdicional DespesaUpdated)
        {
            DespesaAdicional Despesa = await _context.Set<DespesaAdicional>().SingleOrDefaultAsync(v => v.Id == id);
            if (Despesa is null) return BadRequest("Despesa não encontrada!");
            Despesa.Update(DespesaUpdated);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            DespesaAdicional Despesa = await _context.Set<DespesaAdicional>().SingleOrDefaultAsync(v => v.Id == id);
            if (Despesa is null) return BadRequest("Despesa não encontrada!");
            Despesa.SetAsDeleted();
            Despesa.Update(Despesa);
            _context.SaveChanges();
            return Ok();
        }
    }
}
