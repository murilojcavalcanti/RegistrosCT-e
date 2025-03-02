using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CTeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CTeController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("CTe")]
        public async Task<IActionResult> Post(CTe CTe)
        {
            await _context.Set<CTe>().AddAsync(CTe);
            _context.SaveChanges();
            return Created();
        }
        [HttpGet("CTe")]
        public async Task<IActionResult> Get(CTe CTe)
        {
            //calculo de ct-e
            throw new NotImplementedException();
        }
    }
}
