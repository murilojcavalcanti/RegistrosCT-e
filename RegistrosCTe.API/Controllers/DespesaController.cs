using Microsoft.AspNetCore.Mvc;
using RegistrosCTe.API.Persistance;
using RegistrosCTe.Application.Services.DespesasServices;
using RegistrosCTe.Domain.Entities;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesasService _Service;

        public DespesaController(IDespesasService service)
        {
            _Service = service;
        }
        [HttpPost]
        public IActionResult Post(DespesaAdicionalInputModel DespesaModel)
        {
            DespesaAdicional despesa = _Service.Post(DespesaModel);
            return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, DespesaAdicionalViewModel.FromEntity(despesa));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<DespesaAdicionalViewModel> despesasModel = _Service.GetAll(); 
            return Ok(despesasModel);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            DespesaAdicionalViewModelDetails despesaModel = _Service.GetById(id) ;
            return Ok(despesaModel);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, DespesaAdicionalUpdateInputModel DespesaModel)
        {
            _Service.Update(id, DespesaModel);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _Service.Delete(id);
            return Ok();
        }
    }
}
