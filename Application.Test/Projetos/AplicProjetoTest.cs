using Application.Projetos;
using Domain.Enums;
using Domain.Projetos;
using Domain.Projetos.Models;
using Moq;

namespace Application.Test.Projetos
{
    public class AplicProjetoTests
    {
        private readonly Mock<IRepProjeto> _mockRepProjeto;
        private readonly AplicProjeto _aplicProjeto;

        public AplicProjetoTests()
        {
            _mockRepProjeto = new Mock<IRepProjeto>();
            _aplicProjeto = new AplicProjeto(_mockRepProjeto.Object);
        }

        [Fact]
        public async Task AdicionarProjeto_ReturnsProjeto()
        {
            var projeto = new Projeto();
            _mockRepProjeto.Setup(x => x.SaveChangesAsync(projeto)).Returns(Task.CompletedTask);

            var projetoDto = new ProjetoDto()
            {
                Descricao = "Teste",
            };

            var result = await _aplicProjeto.AdicionarProjeto(projetoDto);

            Assert.NotNull(result);
            _mockRepProjeto.Verify(x => x.SaveChangesAsync(It.IsAny<Projeto>()), Times.Once);
        }

        [Fact]
        public async Task AlterarProjeto_ValidId_UpdatesAndReturnsProjeto()
        {
            int projetoId = 1;
            var projetoExistente = new Projeto { Id = projetoId, Descricao = "Descricao antiga" };
            var projetoDto = new ProjetoDto { Descricao = "Descricao nova" };
            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projetoId)).ReturnsAsync(projetoExistente);
            _mockRepProjeto.Setup(x => x.SaveChangesAsync(projetoExistente)).Returns(Task.CompletedTask);

            var result = await _aplicProjeto.AlterarProjeto(projetoId, projetoDto);

            Assert.Equal(projetoDto.Descricao, result.Descricao);
            _mockRepProjeto.Verify(x => x.SaveChangesAsync(projetoExistente), Times.Once);
        }

        [Fact]
        public async Task AlterarProjeto_InvalidId_ThrowsException()
        {
            int projetoId = 99;
            var projetoDto = new ProjetoDto { Descricao = "Descricao nova" };
            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projetoId)).ReturnsAsync((Projeto)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicProjeto.AlterarProjeto(projetoId, projetoDto));
            Assert.Equal($"Projeto de Id: {projetoId} não encontrado.", exception.Message);
        }

        [Fact]
        public async Task ListarProjetos_ReturnsListOfProjetos()
        {
            var projetos = new List<Projeto>
            {
                new() {
                    Id = 1,
                    Descricao = "Projeto 1",
                    DataCriacao = DateTime.Now,
                    Tarefas = []
                },
                new() {
                    Id = 2,
                    Descricao = "Projeto 2",
                    DataCriacao = DateTime.Now,
                    Tarefas = []
                }
            };

            var projetosView = projetos.Select(proj => new ProjetoView(proj)).ToList();
            _mockRepProjeto.Setup(x => x.FindAllAsync()).ReturnsAsync(projetos);

            var result = await _aplicProjeto.ListarProjetos();

            Assert.Equal(projetosView.Count, result.Count);
            for (int i = 0; i < projetosView.Count; i++)
            {
                Assert.Equal(projetosView[i].Id, result[i].Id);
                Assert.Equal(projetosView[i].Descricao, result[i].Descricao);
                Assert.Equal(projetosView[i].DataCriacao, result[i].DataCriacao);
            }
        }


        [Fact]
        public async Task ListarProjetosPorId_ValidId_ReturnsProjeto()
        {
            int projetoId = 1;
            var projeto = new Projeto
            {
                Id = projetoId,
                Descricao = "Descrição de Teste",
                DataCriacao = DateTime.Now,
                Tarefas = []
            };

            var projetoView = new ProjetoView(projeto);
            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projetoId)).ReturnsAsync(projeto);

            var result = await _aplicProjeto.ListarProjetosPorId(projetoId);

            Assert.Equal(projetoView.Id, result.Id);
            Assert.Equal(projetoView.Descricao, result.Descricao);
            Assert.Equal(projetoView.Tarefas.Count, result.Tarefas.Count);
        }


        [Fact]
        public async Task ListarProjetosPorId_InvalidId_ThrowsException()
        {
            int projetoId = 99;
            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projetoId)).ReturnsAsync((Projeto)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicProjeto.ListarProjetosPorId(projetoId));
            Assert.Equal($"Projeto de Id: {projetoId} não encontrado.", exception.Message);
        }

        [Fact]
        public async Task RemoverProjeto_ValidId_RemovesProjeto()
        {
            int projetoId = 1;
            var projeto = new Projeto
            {
                Id = projetoId,
                Tarefas = [new(Prioridade.Alta) { Status = Status.Finalizado }]
            };
            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projetoId)).ReturnsAsync(projeto);
            _mockRepProjeto.Setup(x => x.DeleteByIdAsync(projetoId)).Returns(Task.CompletedTask);
            _mockRepProjeto.Setup(x => x.SaveChangesAsync(projeto)).Returns(Task.CompletedTask);

            await _aplicProjeto.RemoverProjeto(projetoId);

            _mockRepProjeto.Verify(x => x.DeleteByIdAsync(projetoId), Times.Once);
        }

        [Fact]
        public async Task RemoverProjeto_InvalidId_ThrowsException()
        {
            int projetoId = 99;
            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projetoId)).ReturnsAsync((Projeto)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicProjeto.RemoverProjeto(projetoId));
            Assert.Equal($"Projeto de Id: {projetoId} não encontrado.", exception.Message);
        }

        [Fact]
        public async Task RemoverProjeto_CannotRemove_DoesNotCallDelete()
        {
            int projetoId = 1;
            var projeto = new Projeto
            {
                Tarefas = [new(Prioridade.Alta) { Status = Status.Pendente }]
            };

            _mockRepProjeto.Setup(x => x.FindByCodigoAsync(projetoId)).ReturnsAsync(projeto);
            _mockRepProjeto.Setup(x => x.DeleteByIdAsync(projetoId)).Returns(Task.CompletedTask);

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicProjeto.RemoverProjeto(projetoId));
            Assert.Equal("Projeto possui tarefas pentendes. Para removê-lo, é necessário concluir ou remover as tarefas pendentes.", exception.Message);
        }
    }
}