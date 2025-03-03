using Microsoft.AspNetCore.Mvc;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;
using RegistrosCTe.Application.Models.CTeModels;
using RegistrosCTe.Application.Services.CTeService;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CTeController : ControllerBase
    {
        private readonly ICTeService _Service;

        public CTeController(ICTeService service)
        {
            _Service = service;
        }

        [HttpPost]
        public IActionResult Post(CTeInputModel cteModel)
        {
            CTe cte = _Service.Post(cteModel);
            return CreatedAtAction(nameof(GetById), new { id = cte.Id }, CTeViewModelDetails.FromEntity(cte));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CTeViewModel> cteModel = _Service.GetAll();
            return Ok(cteModel);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            CTeViewModelDetails CTeModel = _Service.GetById(id); ;
            return Ok(CTeModel);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _Service.Delete(id);
            return NoContent();
        }

    }
}
