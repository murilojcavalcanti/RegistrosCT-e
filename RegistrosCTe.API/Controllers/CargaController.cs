using Microsoft.AspNetCore.Mvc;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;
using RegistrosCTe.Application.Services.CargaServices;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CargaController : ControllerBase
    {
        private readonly ICargaService _Service;

        public CargaController(ICargaService service)
        {
            _Service = service;
        }

        [HttpPost]
        public IActionResult Post(CargaInputModel cargaModel)
        {
            Carga carga = _Service.Post(cargaModel);
            return CreatedAtAction(nameof(GetById), new { id = carga.Id }, CargaViewModel.FromEntity(carga));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CargaViewModel> cargasModel = _Service.GetAll();
            return Ok(cargasModel);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            CargaViewModelDetails cargaModel = _Service.GetById(id); ;
            return Ok(cargaModel);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, CargaInputModel cargaModel)
        {
            _Service.Update(id,cargaModel);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _Service.Delete(id);
            return NoContent();
        }
    }
}
