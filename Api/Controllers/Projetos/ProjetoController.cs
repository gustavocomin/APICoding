using Application.Projetos;
using Domain.Projetos.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.Projetos
{
    /// <summary>
    /// Controlador responsável pela gestão dos projetos.
    /// </summary>
    /// <remarks>
    /// Este controlador fornece endpoints para criar, ler, atualizar e remover projetos.
    /// Utiliza a interface <see cref="IAplicProjeto"/> para interagir com a lógica de aplicação.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController(IAplicProjeto aplicProjeto) : ControllerBase
    {
        private readonly IAplicProjeto _aplicProjeto = aplicProjeto;

        /// <summary>
        /// Obtém a lista de todos os projetos.
        /// </summary>
        /// <returns>Uma lista de projetos (IEnumerable&lt;ProjetoDto&gt;).</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Listar todos os projetos")]
        [ProducesResponseType(typeof(IEnumerable<ProjetoView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var projetos = await _aplicProjeto.ListarProjetos();
                return Ok(projetos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtém um projeto específico pelo seu ID.
        /// </summary>
        /// <param name="idProjeto">ID do projeto a ser obtido.</param>
        /// <returns>O projeto correspondente ao ID fornecido (ProjetoDto).</returns>
        [HttpGet("{idProjeto}")]
        [SwaggerOperation(Summary = "Obter projeto por ID")]
        [ProducesResponseType(typeof(ProjetoView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int idProjeto)
        {
            try
            {
                var projeto = await _aplicProjeto.ListarProjetosPorId(idProjeto);
                return Ok(projeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo projeto.
        /// </summary>
        /// <param name="projetoDto">Os dados do projeto a serem adicionados.</param>
        /// <returns>O projeto adicionado (ProjetoDto).</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar novo projeto")]
        [ProducesResponseType(typeof(ProjetoView), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarProjeto([FromBody] ProjetoDto projetoDto)
        {
            try
            {
                var projeto = await _aplicProjeto.AdicionarProjeto(projetoDto);
                return CreatedAtAction(nameof(GetById), new { idProjeto = projeto.Id }, projeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Altera os dados de um projeto existente.
        /// </summary>
        /// <param name="idProjeto">ID do projeto a ser alterado.</param>
        /// <param name="projetoDto">Os novos dados do projeto.</param>
        /// <returns>O projeto alterado (ProjetoDto).</returns>
        [HttpPut("{idProjeto}")]
        [SwaggerOperation(Summary = "Alterar projeto existente")]
        [ProducesResponseType(typeof(ProjetoView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarProjeto([FromRoute] int idProjeto, [FromBody] ProjetoDto projetoDto)
        {
            try
            {
                var projeto = await _aplicProjeto.AlterarProjeto(idProjeto, projetoDto);
                return Ok(projeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Remove um projeto existente.
        /// </summary>
        /// <param name="idProjeto">ID do projeto a ser removido.</param>
        /// <returns>Um resultado 204 No Content se a remoção for bem-sucedida.</returns>
        [HttpDelete("{idProjeto}")]
        [SwaggerOperation(Summary = "Remover projeto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int idProjeto)
        {
            try
            {
                await _aplicProjeto.RemoverProjeto(idProjeto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
