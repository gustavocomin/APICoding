using Api.Controllers.Projetos;
using Application.Projetos;
using Domain.Projetos.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Test.Controllers.Projetos
{
    public class ProjetoControllerTests
    {
        private readonly ProjetoController _controller;
        private readonly Mock<IAplicProjeto> _mockAplicProjeto;

        public ProjetoControllerTests()
        {
            _mockAplicProjeto = new Mock<IAplicProjeto>();
            _controller = new ProjetoController(_mockAplicProjeto.Object);
        }

        [Fact]
        public async Task Get_ReturnsListOfProjetos()
        {
            var projetos = new List<ProjetoView>
            {
                new() {
                    Id = 1,
                    Descricao = "Projeto 1"
                },
                new() {
                    Id = 2,
                    Descricao = "Projeto 2"
                }
            };

            _mockAplicProjeto.Setup(x => x.ListarProjetos()).ReturnsAsync(projetos);

            var result = await _controller.Get() as OkObjectResult;

            Assert.NotNull(result);
            var resultProjetos = Assert.IsAssignableFrom<List<ProjetoView>>(result.Value);
            Assert.Collection(resultProjetos,
                item => Assert.Equal(projetos[0], item),
                item => Assert.Equal(projetos[1], item));
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsProjeto()
        {
            int projetoId = 1;
            var projeto = new ProjetoView();
            _mockAplicProjeto.Setup(x => x.ListarProjetosPorId(projetoId)).ReturnsAsync(projeto);

            var result = await _controller.GetById(projetoId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(projeto, okResult.Value);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsBadRequest()
        {
            int projetoId = 99;
            _mockAplicProjeto.Setup(x => x.ListarProjetosPorId(projetoId)).ThrowsAsync(new Exception("Projeto not found"));

            var result = await _controller.GetById(projetoId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Projeto not found", badRequestResult.Value);
        }

        [Fact]
        public async Task Post_ReturnsOk()
        {
            var projeto = new ProjetoView();
            var projetoDto = new ProjetoDto()
            {
                Descricao = "Teste"
            };

            _mockAplicProjeto.Setup(x => x.AdicionarProjeto(projetoDto)).ReturnsAsync(projeto);

            var result = await _controller.AdicionarProjeto(projetoDto);

            var okResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(projeto, okResult.Value);
        }

        [Fact]
        public async Task Post_WhenException_ReturnsBadRequest()
        {
            var projetoDto = new ProjetoDto()
            {
                Descricao = "Teste"
            };
            _mockAplicProjeto.Setup(x => x.AdicionarProjeto(projetoDto)).ThrowsAsync(new Exception("Error adding projeto"));

            var result = await _controller.AdicionarProjeto(projetoDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error adding projeto", badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOk()
        {
            int projetoId = 1;
            var projeto = new ProjetoView();
            var projetoDto = new ProjetoDto
            {
                Descricao = "Teste"
            };

            _mockAplicProjeto.Setup(x => x.AlterarProjeto(projetoId, projetoDto)).ReturnsAsync(projeto);

            var result = await _controller.AlterarProjeto(projetoId, projetoDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(projeto, okResult.Value);
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsBadRequest()
        {
            int projetoId = 99;

            var projetoDto = new ProjetoDto
            {
                Descricao = "Teste"
            };

            _mockAplicProjeto.Setup(x => x.AlterarProjeto(projetoId, projetoDto)).ThrowsAsync(new Exception("Projeto not found"));

            var result = await _controller.AlterarProjeto(projetoId, projetoDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Projeto not found", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsNoContent()
        {
            int projetoId = 1;
            _mockAplicProjeto.Setup(x => x.RemoverProjeto(projetoId)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(projetoId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsBadRequest()
        {
            int projetoId = 99;
            _mockAplicProjeto.Setup(x => x.RemoverProjeto(projetoId)).ThrowsAsync(new Exception("Projeto not found"));

            var result = await _controller.Delete(projetoId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Projeto not found", badRequestResult.Value);
        }
    }
}