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

        /// <summary>
        /// Adiciona uma nova carga ao banco de dados. 
        /// Obs: Peso em tonelada
        /// </summary>
        /// <param name="cargaModel">Objeto com os campos necessários para criação de uma carga</param>
        /// <returns>Retorna a carga criada.</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        /// <response code="400">Caso os dados informados sejam inválidos</response>
        /// <response code="500">Caso ocorra um erro interno no servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(CargaViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CargaInputModel cargaModel)
        {
            if (cargaModel == null || !ModelState.IsValid)
            {
                return BadRequest("Os dados da carga são inválidos.");
            }
            try
            {
                CargaViewModel carga = await _Service.Post(cargaModel);
                return CreatedAtAction(nameof(GetById), new { id = carga.CargaId }, carga);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todas as cargas cadastradas no banco de dados
        /// </summary>
        /// <returns>Lista de cargas</returns>
        /// <response code="200">Caso a requisição seja feita com sucesso</response>
        /// <response code="204">Caso não existam cargas cadastradas</response>
        /// <response code="500">Caso ocorra um erro interno no servidor</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CargaViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CargaViewModel> cargasModel = await _Service.GetAll();

                if (cargasModel == null || !cargasModel.Any())
                {
                    return NoContent();
                }

                return Ok(cargasModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna uma carga específica com base no ID fornecido
        /// </summary>
        /// <param name="id">Identificador da carga a ser buscada</param>
        /// <returns>Dados da carga encontrada.</returns>
        /// <response code="200">Caso a requisição seja feita com sucesso</response>
        /// <response code="404">Caso a carga com o ID especificado não seja encontrada</response>
        /// <response code="500">Caso ocorra um erro interno no servidor</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CargaViewModelDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                CargaViewModelDetails cargaModel = await _Service.GetById(id);

                if (cargaModel == null)
                {
                    return NotFound($"Nenhuma carga encontrada com o ID {id}.");
                }

                return Ok(cargaModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza uma carga existente no banco de dados
        /// </summary>
        /// <param name="id">Identificador da carga que será atualizada</param>
        /// <param name="cargaModel">Objeto contendo os novos dados para a carga</param>
        /// <returns>Status da operação</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        /// <response code="400">Caso os dados informados sejam inválidos</response>
        /// <response code="500">Caso ocorra um erro interno no servidor</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, CargaInputModel cargaModel)
        {
            if (cargaModel == null || !ModelState.IsValid)
            {
                return BadRequest("Os dados da carga são inválidos.");
            }

            try
            {
                await _Service.Update(id, cargaModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }
        /// <summary>
        /// Remove uma carga do banco de dados
        /// </summary>
        /// <param name="id">Identificador da carga que será removida</param>
        /// <returns>Status da operação</returns>
        /// <response code="204">Caso a remoção seja feita com sucesso</response>
        /// <response code="500">Caso ocorra um erro interno no servidor</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _Service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }
    }
}
