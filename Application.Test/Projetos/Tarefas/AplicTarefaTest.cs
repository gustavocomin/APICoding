using Application.Tarefas.Tarefas;
using Domain.Enums;
using Domain.Projetos;
using Domain.Projetos.Models;
using Domain.Projetos.Tarefas;
using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Domain.Projetos.Tarefas.Comentarios;
using Domain.Projetos.Tarefas.Comentarios.Models;
using Domain.Projetos.Tarefas.Models;
using Domain.Usuarios;
using Moq;

namespace Application.Tests.Tarefas
{
    public class AplicTarefaTests
    {
        private readonly Mock<IRepTarefa> _mockRepTarefa;
        private readonly Mock<IRepUsuario> _mockRepUsuario;
        private readonly Mock<IRepAtualizacaoTarefa> _mockRepAtualizacaoTarefa;
        private readonly Mock<IRepComentario> _mockRepComentario;
        private readonly Mock<IRepProjeto> _mockRepProjeto;
        private readonly AplicTarefa _aplicTarefa;

        public AplicTarefaTests()
        {
            _mockRepTarefa = new Mock<IRepTarefa>();
            _mockRepUsuario = new Mock<IRepUsuario>();
            _mockRepAtualizacaoTarefa = new Mock<IRepAtualizacaoTarefa>();
            _mockRepComentario = new Mock<IRepComentario>();
            _mockRepProjeto = new Mock<IRepProjeto>();
            _aplicTarefa = new AplicTarefa(_mockRepTarefa.Object,
                                           _mockRepUsuario.Object,
                                           _mockRepAtualizacaoTarefa.Object,
                                           _mockRepComentario.Object,
                                           _mockRepProjeto.Object);
        }

        [Fact]
        public async Task AdicionarTarefa_ReturnsNewTarefa_WhenSuccessful()
        {
            var prioridade = Prioridade.Alta;
            var loginUsuario = "usuario1";
            var usuario = new Usuario
            {
                Login = loginUsuario
            };

            var tarefa = new Tarefa(prioridade);

            var projeto = new Projeto()
            {
                Id = 1,
            };

            _mockRepUsuario.Setup(x => x.GetUserByLogin(loginUsuario)).ReturnsAsync(usuario);
            _mockRepTarefa.Setup(x => x.SaveChangesAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);
            _mockRepAtualizacaoTarefa.Setup(x => x.SaveChangesAsync(It.IsAny<AtualizacaoTarefa>())).Returns(Task.CompletedTask);
            _mockRepComentario.Setup(x => x.SaveChangesAsync(It.IsAny<Comentario>())).Returns(Task.CompletedTask);
            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projeto.Id)).ReturnsAsync(projeto);

            var tarefaDto = new TarefaDto()
            {
                LoginUsuario = loginUsuario,
                Prioridade = prioridade,
                IdProjeto = 1
            };

            var result = await _aplicTarefa.AdicionarTarefa(tarefaDto);

            Assert.NotNull(result);
            Assert.Equal(prioridade, result.Prioridade);
        }

        [Fact]
        public async Task AlterarTarefa_UpdatesAndReturnsTarefa_WhenSuccessful()
        {
            var idTarefa = 1;
            var usuario = new Usuario
            {
                Login = "usuario1"
            };

            var tarefaDto = new TarefaDto()
            {
                Status = Status.Finalizado,
                LoginUsuario = usuario.Login
            };

            var tarefaExistente = new Tarefa(Prioridade.Baixa)
            {
                Id = idTarefa,
                Status = Status.Ativo
            };

            _mockRepTarefa.Setup(x => x.FindByCodigoAsync(idTarefa)).ReturnsAsync(tarefaExistente);
            _mockRepUsuario.Setup(x => x.GetUserByLogin(usuario.Login)).ReturnsAsync(usuario);
            _mockRepAtualizacaoTarefa.Setup(x => x.SaveChangesAsync(It.IsAny<AtualizacaoTarefa>())).Returns(Task.CompletedTask);
            _mockRepComentario.Setup(x => x.SaveChangesAsync(It.IsAny<Comentario>())).Returns(Task.CompletedTask);
            _mockRepTarefa.Setup(x => x.SaveChangesAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);

            var result = await _aplicTarefa.AlterarTarefa(idTarefa, tarefaDto);

            Assert.Equal(tarefaDto.Status, result.Status);
        }

        [Fact]
        public async Task ListarTarefas_ReturnsListOfTarefas()
        {
            var tarefas = new List<Tarefa>
            {
                new(Prioridade.Alta),
                new(Prioridade.Baixa)
            };
            _mockRepTarefa.Setup(x => x.FindAllAsync()).ReturnsAsync(tarefas);

            var result = await _aplicTarefa.ListarTarefas();

            Assert.Equal(tarefas.Count, result.Count);
        }

        [Fact]
        public async Task ListarTarefasPorId_ReturnsTarefa_WhenFound()
        {
            var idTarefa = 1;
            var tarefa = new Tarefa(Prioridade.Alta) { Id = idTarefa };
            var tarefaView = new TarefaView() { Id = idTarefa };
            _mockRepTarefa.Setup(x => x.FindByCodigoAsync(idTarefa)).ReturnsAsync(tarefa);

            var result = await _aplicTarefa.ListarTarefasPorId(idTarefa);

            Assert.NotNull(result);
            Assert.Equal(tarefaView.Id, result.Id);
        }

        [Fact]
        public async Task ListarTarefasPorId_ThrowsException_WhenNotFound()
        {
            var idTarefa = 1;
            _mockRepTarefa.Setup(x => x.FindByCodigoAsync(idTarefa)).ReturnsAsync((Tarefa)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicTarefa.ListarTarefasPorId(idTarefa));
            Assert.Equal($"Tarefa de Id: {idTarefa} não encontrado.", exception.Message);
        }

        [Fact]
        public async Task RemoverTarefa_DeletesTarefa_WhenSuccessful()
        {
            var idTarefa = 1;
            var usuario = new Usuario { Login = "usuario1" };
            var tarefa = new Tarefa(Prioridade.Alta) { Id = idTarefa };

            _mockRepTarefa.Setup(x => x.FindByCodigoAsync(idTarefa)).ReturnsAsync(tarefa);
            _mockRepUsuario.Setup(x => x.GetUserByLogin(usuario.Login)).ReturnsAsync(usuario);
            _mockRepAtualizacaoTarefa.Setup(x => x.SaveChangesAsync(It.IsAny<AtualizacaoTarefa>())).Returns(Task.CompletedTask);
            _mockRepComentario.Setup(x => x.SaveChangesAsync(It.IsAny<Comentario>())).Returns(Task.CompletedTask);
            _mockRepTarefa.Setup(x => x.DeleteByIdAsync(idTarefa)).Returns(Task.CompletedTask);

            await _aplicTarefa.RemoverTarefa(idTarefa, usuario.Login);

            _mockRepTarefa.Verify(x => x.DeleteByIdAsync(idTarefa), Times.Once);
        }

        [Fact]
        public async Task RemoverTarefa_ThrowsException_WhenTarefaNotFound()
        {
            var idTarefa = 1;
            var usuario = new Usuario { Login = "usuario1" };
            _mockRepTarefa.Setup(x => x.FindByCodigoAsync(idTarefa)).ReturnsAsync((Tarefa)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicTarefa.RemoverTarefa(idTarefa, usuario.Login));
            Assert.Equal($"Tarefa de Id: {idTarefa} não encontrado.", exception.Message);
        }
    }
}
