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

        /// <summary>
        /// Adiciona uma nova Despesa Adicional ao banco de dados.
        /// </summary>
        /// <param name="DespesaModel">Objeto contendo os campos necessários para a criação de uma Despesa Adicional.</param>
        /// <returns>Retorna a despesa criada.</returns>
        /// <response code="201">Caso a despesa seja inserida com sucesso.</response>
        /// <response code="400">Caso os dados informados sejam inválidos.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DespesaAdicionalViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(DespesaAdicionalInputModel DespesaModel)
        {
            if (DespesaModel == null || !ModelState.IsValid)
            {
                return BadRequest("Os dados da despesa são inválidos.");
            }

            try
            {
                DespesaAdicional despesa = _Service.Post(DespesaModel);
                return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, DespesaAdicionalViewModel.FromEntity(despesa));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todas as Despesas Adicionais cadastradas no banco de dados.
        /// </summary>
        /// <returns>Lista de despesas adicionais.</returns>
        /// <response code="200">Caso existam despesas cadastradas.</response>
        /// <response code="204">Caso não existam despesas cadastradas.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DespesaAdicionalViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                List<DespesaAdicionalViewModel> despesasModel = _Service.GetAll();

                if (despesasModel == null || !despesasModel.Any())
                {
                    return NoContent();
                }

                return Ok(despesasModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna uma Despesa Adicional específica com base no ID fornecido.
        /// </summary>
        /// <param name="id">Identificador da despesa a ser buscada.</param>
        /// <returns>Dados da despesa adicional encontrada.</returns>
        /// <response code="200">Caso a despesa seja encontrada.</response>
        /// <response code="404">Caso a despesa com o ID especificado não seja encontrada.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DespesaAdicionalViewModelDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                DespesaAdicionalViewModelDetails despesaModel = _Service.GetById(id);

                if (despesaModel == null)
                {
                    return NotFound($"Nenhuma despesa encontrada com o ID {id}.");
                }

                return Ok(despesaModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza os dados de uma Despesa Adicional no banco de dados com base no ID fornecido.
        /// </summary>
        /// <param name="id">Identificador da despesa a ser atualizada.</param>
        /// <param name="DespesaModel">Objeto contendo os novos dados da despesa.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Caso a atualização seja realizada com sucesso.</response>
        /// <response code="400">Caso os dados informados sejam inválidos.</response>
        /// <response code="500">Caso ocorra um erro interno no servidor.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, DespesaAdicionalUpdateInputModel DespesaModel)
        {
            if (DespesaModel == null || !ModelState.IsValid)
            {
                return BadRequest("Os dados da despesa são inválidos.");
            }

            try
            {
                _Service.Update(id, DespesaModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui uma Despesa Adicional do banco de dados com base no ID fornecido.
        /// </summary>
        /// <param name="id">Identificador da despesa a ser excluída.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Caso a despesa seja excluída com sucesso.</response>
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
