using Application.Relatorios.Desempenho;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.Relatorios.Desempenho
{
    /// <summary>
    /// Controlador responsável pela gestão dos relatórios.
    /// </summary>
    /// <remarks>
    /// Este controlador fornece endpoints para gerar relatórios.
    /// Utiliza a interface <see cref="IAplicRelDesempenho"/> para interagir com a lógica de aplicação.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioDesempenhoController(IAplicRelDesempenho aplicRelDesempenho) : ControllerBase
    {
        private readonly IAplicRelDesempenho _aplicRelDesempenho = aplicRelDesempenho;

        /// <summary>
        /// Gera relatório de tarefas finalizadas por usuários nos últimos 30 dias.
        /// </summary>
        /// <returns>Valor da média.</returns>
        [HttpGet("{loginUsuario}")]
        [SwaggerOperation(Summary = "Gerar média de tarefas finalizads por úsuario")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] string loginUsuario)
        {
            try
            {
                return Ok(await _aplicRelDesempenho.GerarMediaTarefasPorUsuario(loginUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}