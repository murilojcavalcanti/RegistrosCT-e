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

        /// <summary>
        /// Adiciona um novo Conhecimento de Transporte Eletrônico (CT-e) ao banco de dados.
        /// </summary>
        /// <param name="cteModel">Objeto contendo os campos necessários para a criação de um CT-e.</param>
        /// <returns>Retorna o CT-e criado.</returns>
        /// <response code="201">Caso o CT-e seja inserido com sucesso.</response>
        /// <response code="400">Caso os dados informados sejam inválidos.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CTeViewModelDetails), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post(CTeInputModel cteModel)
        {
            if (cteModel == null || !ModelState.IsValid)
            {
                return BadRequest("Os dados do CT-e são inválidos.");
            }

            try
            {
                CTe cte = _Service.Post(cteModel);
                return CreatedAtAction(nameof(GetById), new { id = cte.Id }, CTeViewModelDetails.FromEntity(cte));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todos os Conhecimentos de Transporte Eletrônico (CT-es) cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de CT-es.</returns>
        /// <response code="200">Caso existam CT-es cadastrados.</response>
        /// <response code="204">Caso não existam CT-es cadastrados.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CTeViewModel>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult GetAll()
        {
            try
            {
                List<CTeViewModel> cteModel = _Service.GetAll();

                if (cteModel == null || !cteModel.Any())
                {
                    return NoContent();
                }

                return Ok(cteModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna um Conhecimento de Transporte Eletrônico (CT-e) específico com base no ID fornecido.
        /// </summary>
        /// <param name="id">Identificador do CT-e a ser buscado.</param>
        /// <returns>Dados do CT-e encontrado.</returns>
        /// <response code="200">Caso o CT-e seja encontrado.</response>
        /// <response code="404">Caso o CT-e com o ID especificado não seja encontrado.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CTeViewModelDetails), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetById(int id)
        {
            try
            {
                CTeViewModelDetails CTeModel = _Service.GetById(id);

                if (CTeModel == null)
                {
                    return NotFound($"Nenhum CT-e encontrado com o ID {id}.");
                }

                return Ok(CTeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui um Conhecimento de Transporte Eletrônico (CT-e) do banco de dados com base no ID fornecido.
        /// </summary>
        /// <param name="id">Identificador do CT-e a ser excluído.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Caso o CT-e seja excluído com sucesso.</response>
        /// <response code="404">Caso o CT-e com o ID especificado não seja encontrado.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            try
            {
                var cteExistente = _Service.GetById(id);
                if (cteExistente == null)
                {
                    return NotFound($"Nenhum CT-e encontrado com o ID {id}.");
                }

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
