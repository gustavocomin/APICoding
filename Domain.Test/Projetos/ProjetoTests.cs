using Domain.Enums;
using Domain.Projetos.Models;
using Domain.Projetos.Tarefas.Models;

namespace Domain.Test.Projetos
{
    public class ProjetoTests
    {
        [Fact]
        public void AdicionarTarefas_ComTarefasValidas_AdicionaTarefas()
        {
            var projeto = new Projeto()
            {
                Tarefas = []
            };
            var tarefas = new List<Tarefa>
            {
                new(Prioridade.Alta),
                new(Prioridade.Media)
            };

            projeto.PodeAdicionarTarefas();

            Assert.True(projeto.PodeAdicionarTarefas());
        }

        [Fact]
        public void AdicionarTarefas_QuandoExcedeLimite_ThrowArgumentException()
        {
            var projeto = new Projeto
            {
                Tarefas = []
            };

            for (int i = 0; i < 20; i++)
            {
                projeto.Tarefas.Add(new Tarefa(Prioridade.Alta));
            }
            var novasTarefas = new List<Tarefa>
            {
                new(Prioridade.Media)
            };

            var exception = Assert.Throws<ArgumentException>(() => projeto.PodeAdicionarTarefas());
            Assert.Equal("Um projeto só pode ter até 20 tarefas.", exception.Message);
        }

        [Fact]
        public void PodeRemoverProjeto_QuandoTarefasPendentes_ThrowException()
        {
            var projeto = new Projeto
            {
                Tarefas =
                [
                    new(Prioridade.Alta) { Status = Status.Pendente }
                ]
            };

            var exception = Assert.Throws<Exception>(() => projeto.PodeRemoverProjeto());
            Assert.Equal("Projeto possui tarefas pentendes. Para removê-lo, é necessário concluir ou remover as tarefas pendentes.", exception.Message);
        }

        [Fact]
        public void PodeRemoverProjeto_QuandoNaoHaTarefasPendentes_ReturnsTrue()
        {
            var projeto = new Projeto
            {
                Tarefas =
                [
                    new(Prioridade.Alta) { Status = Status.Finalizado }
                ]
            };

            var result = projeto.PodeRemoverProjeto();

            Assert.True(result);
        }
    }
}