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
        /// <summary>
        /// Adiciona uma nova viagem ao banco de dados.
        /// </summary>
        /// <param name="model">Objeto contendo os campos necessários para a criação de uma viagem.</param>
        /// <returns>Retorna a viagem criada.</returns>
        /// <response code="201">Caso a viagem seja inserida com sucesso.</response>
        /// <response code="400">Caso os dados informados sejam inválidos.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ViagemViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(ViagemInputModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Os dados da viagem são inválidos.");
            }

            try
            {
                Viagem viagem = _Service.Post(model);
                return CreatedAtAction(nameof(GetById), new { id = viagem.Id }, ViagemViewModel.FromEntity(viagem));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todas as viagens cadastradas no banco de dados.
        /// </summary>
        /// <returns>Lista de viagens.</returns>
        /// <response code="200">Caso existam viagens cadastradas.</response>
        /// <response code="204">Caso não existam viagens cadastradas.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViagemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                List<ViagemViewModel> viagemViewModel = _Service.GetAll();

                if (viagemViewModel == null || !viagemViewModel.Any())
                {
                    return NoContent();
                }

                return Ok(viagemViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna uma viagem específica com base no ID fornecido.
        /// </summary>
        /// <param name="id">Identificador da viagem a ser buscada.</param>
        /// <returns>Dados da viagem encontrada.</returns>
        /// <response code="200">Caso a viagem seja encontrada.</response>
        /// <response code="404">Caso a viagem com o ID especificado não seja encontrada.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ViagemViewModelDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                ViagemViewModelDetails viagemModel = _Service.GetById(id);

                if (viagemModel == null)
                {
                    return NotFound($"Nenhuma viagem encontrada com o ID {id}.");
                }

                return Ok(viagemModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza os dados de uma viagem existente no banco de dados.
        /// </summary>
        /// <param name="id">Identificador da viagem a ser atualizada.</param>
        /// <param name="viagemModel">Objeto contendo os novos dados da viagem.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso.</response>
        /// <response code="404">Caso a viagem com o ID especificado não seja encontrada.</response>
        /// <response code="400">Caso os dados informados sejam inválidos.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, ViagemUpdateInputModel viagemModel)
        {
            if (viagemModel == null || !ModelState.IsValid)
            {
                return BadRequest("Os dados da viagem são inválidos.");
            }

            try
            {
                _Service.Update(id, viagemModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove uma viagem do banco de dados.
        /// </summary>
        /// <param name="id">Identificador da viagem a ser removida.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Caso a remoção seja feita com sucesso.</response>
        /// <response code="404">Caso a viagem com o ID especificado não seja encontrada.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                _Service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }
    }
}
