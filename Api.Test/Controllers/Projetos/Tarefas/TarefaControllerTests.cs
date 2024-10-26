using Api.Controllers.Projetos.Tarefas;
using Application.Projetos.Tarefas;
using Domain.Enums;
using Domain.Projetos.Tarefas.Models;
using Domain.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Test.Controllers.Projetos.Tarefas
{
    public class TarefaControllerTests
    {
        private readonly Mock<IAplicTarefa> _mockAplicTarefa;
        private readonly TarefaController _controller;

        public TarefaControllerTests()
        {
            _mockAplicTarefa = new Mock<IAplicTarefa>();
            _controller = new TarefaController(_mockAplicTarefa.Object);
        }

        [Fact]
        public async Task Get_ReturnsTarefasList()
        {
            var tarefas = new List<TarefaView> {
                new()
                {
                    Prioridade = Prioridade.Alta
                },
                 new()
                {
                    Prioridade = Prioridade.Baixa
                }
            };

            _mockAplicTarefa.Setup(x => x.ListarTarefas()).ReturnsAsync(tarefas);

            var result = await _controller.Get() as OkObjectResult;

            Assert.NotNull(result);
            var resultTarefas = Assert.IsAssignableFrom<List<TarefaView>>(result.Value);
            Assert.Collection(resultTarefas,
                item => Assert.Equal(tarefas[0], item),
                item => Assert.Equal(tarefas[1], item));
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOkWithTarefa()
        {
            int tarefaId = 1;
            var tarefa = new TarefaView()
            {
                Prioridade = Prioridade.Media
            };

            _mockAplicTarefa.Setup(x => x.ListarTarefasPorId(tarefaId)).ReturnsAsync(tarefa);

            var result = await _controller.GetById(tarefaId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tarefa, okResult.Value);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsBadRequest()
        {
            int tarefaId = 99;
            _mockAplicTarefa.Setup(x => x.ListarTarefasPorId(tarefaId)).ThrowsAsync(new Exception("Tarefa not found"));

            var result = await _controller.GetById(tarefaId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Tarefa not found", badRequestResult.Value);
        }

        [Fact]
        public async Task Post_ValidPrioridade_ReturnsOkWithTarefa()
        {
            var prioridade = Prioridade.Alta;
            var tarefa = new TarefaView()
            {
                Prioridade = Prioridade.Alta
            };

            var usuario = "login1";
            var tarefaDto = new TarefaDto()
            {
                IdProjeto = 1,
                LoginUsuario = usuario,
                Prioridade = prioridade,
            };

            _mockAplicTarefa.Setup(x => x.AdicionarTarefa(tarefaDto)).ReturnsAsync(tarefa);

            var result = await _controller.AdicionarTarefa(tarefaDto);

            var okResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(tarefa, okResult.Value);
        }

        [Fact]
        public async Task Post_Exception_ReturnsBadRequest()
        {
            var usuario = "login1";
            var prioridade = Prioridade.Media;
            var tarefaDto = new TarefaDto()
            {
                IdProjeto = 1,
                LoginUsuario = usuario,
                Prioridade = prioridade,
            };

            _mockAplicTarefa.Setup(x => x.AdicionarTarefa(tarefaDto)).ThrowsAsync(new Exception("Error creating tarefa"));

            var result = await _controller.AdicionarTarefa(tarefaDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error creating tarefa", badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOkWithUpdatedTarefa()
        {
            int tarefaId = 1;
            var tarefaDto = new TarefaDto
            {
                LoginUsuario = "usuario_teste",
                Status = Status.Ativo
            };

            var usuario = new Usuario
            {
                Login = "usuario_teste",
            };

            var tarefaView = new TarefaView
            {
                Id = tarefaId,
                Status = Status.Finalizado
            };

            _mockAplicTarefa.Setup(x => x.AlterarTarefa(tarefaId, tarefaDto)).ReturnsAsync(tarefaView);

            var result = await _controller.AlterarTarefa(tarefaId, tarefaDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tarefaView, okResult.Value);
        }


        [Fact]
        public async Task Delete_ValidId_ReturnsNoContent()
        {
            int tarefaId = 1;
            var usuario = "login1";
            _mockAplicTarefa.Setup(x => x.RemoverTarefa(tarefaId, usuario)).Returns(Task.CompletedTask);

            var result = await _controller.RemoverTarefa(tarefaId, usuario);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsBadRequest()
        {
            int tarefaId = 99;
            var usuario = "login1";
            _mockAplicTarefa.Setup(x => x.RemoverTarefa(tarefaId, usuario)).ThrowsAsync(new Exception("Tarefa not found"));

            var result = await _controller.RemoverTarefa(tarefaId, usuario);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Tarefa not found", badRequestResult.Value);
        }
    }
}