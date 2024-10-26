using Api.Controllers.Relatorios.Desempenho;
using Application.Relatorios.Desempenho;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Test.Controllers.Relatorios.Desempenho
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioDesempenhoControllerTests
    {
        private readonly Mock<IAplicRelDesempenho> _mockAplicRelDesempenho;
        private readonly RelatorioDesempenhoController _controller;

        public RelatorioDesempenhoControllerTests()
        {
            _mockAplicRelDesempenho = new Mock<IAplicRelDesempenho>();
            _controller = new RelatorioDesempenhoController(_mockAplicRelDesempenho.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenSuccessful()
        {
            var loginUsuario = "usuario1";
            var mediaTarefas = 5.0;
            _mockAplicRelDesempenho.Setup(x => x.GerarMediaTarefasPorUsuario(loginUsuario))
                 .ReturnsAsync(mediaTarefas);


            var result = await _controller.GetById(loginUsuario);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mediaTarefas, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsBadRequest_WhenExceptionThrown()
        {
            var loginUsuario = "usuario1";
            _mockAplicRelDesempenho.Setup(x => x.GerarMediaTarefasPorUsuario(loginUsuario))
                .ThrowsAsync(new Exception("Erro ao gerar relatório"));

            var result = await _controller.GetById(loginUsuario);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro ao gerar relatório", badRequestResult.Value);
        }
    }
}
