using Microsoft.AspNetCore.Mvc;
using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Application.Services.ViagemService;
using RegistrosCTe.Domain.Entities;

namespace RegistrosCTe.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ViagemController : ControllerBase
    {
        private readonly IViagemService _Service;

        public ViagemController(IViagemService service)
        {
            _Service = service;
        }

        [HttpPost]
        public IActionResult Post(ViagemInputModel model)
        {
            Viagem viagem = _Service.Post(model);
            return CreatedAtAction(nameof(GetById), new {id =viagem.Id}, ViagemViewModel.FromEntity(viagem));
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ViagemViewModel> viagemViewModel = _Service.GetAll();
            return Ok(viagemViewModel);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            ViagemViewModelDetails viagemModel = _Service.GetById(id);
            return Ok(viagemModel);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id,ViagemUpdateInputModel viagemModel)
        {
            _Service.Update(id, viagemModel);
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
