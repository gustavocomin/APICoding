using Application.Projetos.Tarefas;
using Domain.Projetos.Tarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.Projetos.Tarefas
{
    /// <summary>
    /// Controlador responsável pela gestão das tarefas.
    /// </summary>
    /// <remarks>
    /// Este controlador fornece endpoints para criar, ler, atualizar e remover tarefas.
    /// Utiliza a interface <see cref="IAplicTarefa"/> para interagir com a lógica de aplicação.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController(IAplicTarefa aplicTarefa) : ControllerBase
    {
        private readonly IAplicTarefa _aplicTarefa = aplicTarefa;

        /// <summary>
        /// Obtém a lista de tarefas.
        /// </summary>
        /// <returns>Uma lista de tarefas.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Listar todas as tarefas")]
        [ProducesResponseType(typeof(IEnumerable<TarefaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tarefas = await _aplicTarefa.ListarTarefas();
                return Ok(tarefas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtém uma tarefa pelo ID.
        /// </summary>
        /// <param name="idTarefa">ID da tarefa.</param>
        /// <returns>A tarefa correspondente ao ID fornecido.</returns>
        [HttpGet("{idTarefa}")]
        [SwaggerOperation(Summary = "Obter tarefa por ID")]
        [ProducesResponseType(typeof(TarefaView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int idTarefa)
        {
            try
            {
                var tarefa = await _aplicTarefa.ListarTarefasPorId(idTarefa);
                return Ok(tarefa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adiciona uma nova tarefa.
        /// </summary>
        /// <param name="tarefaDto">Os dados da tarefa a ser adicionada.</param>
        /// <returns>A tarefa adicionada.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar nova tarefa")]
        [ProducesResponseType(typeof(TarefaView), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarTarefa([FromBody] TarefaDto tarefaDto)
        {
            try
            {
                var tarefa = await _aplicTarefa.AdicionarTarefa(tarefaDto);
                return CreatedAtAction(nameof(GetById), new { idTarefa = tarefa.Id }, tarefa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Altera uma tarefa existente.
        /// </summary>
        /// <param name="idTarefa">ID da tarefa a ser alterada.</param>
        /// <param name="tarefaDto">Os novos dados da tarefa.</param>
        /// <returns>A tarefa alterada.</returns>
        [HttpPut("{idTarefa}")]
        [SwaggerOperation(Summary = "Alterar tarefa existente")]
        [ProducesResponseType(typeof(TarefaView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarTarefa([FromRoute] int idTarefa, [FromBody] TarefaDto tarefaDto)
        {
            try
            {
                var tarefa = await _aplicTarefa.AlterarTarefa(idTarefa, tarefaDto);
                return Ok(tarefa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Remove uma tarefa.
        /// </summary>
        /// <param name="idTarefa">ID da tarefa a ser removida.</param>
        /// <param name="loginUsuario">Login do usuário que está removendo a tarefa.</param>
        /// <returns>NoContent se a tarefa foi removida com sucesso.</returns>
        [HttpDelete("{idTarefa}/{loginUsuario}")]
        [SwaggerOperation(Summary = "Remover tarefa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoverTarefa([FromRoute] int idTarefa, [FromRoute] string loginUsuario)
        {
            try
            {
                await _aplicTarefa.RemoverTarefa(idTarefa, loginUsuario);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
