using Application.Relatorios.Desempenho;
using Domain.Enums;
using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Domain.Projetos.Tarefas.Models;
using Domain.Usuarios;
using Moq;

namespace Application.Tests.Relatorios.Desempenho
{
    public class AplicRelDesempenhoTests
    {
        private readonly Mock<IRepAtualizacaoTarefa> _mockRepAtualizacaoTarefa;
        private readonly Mock<IRepUsuario> _mockRepUsuario;
        private readonly AplicRelDesempenho _aplicRelDesempenho;

        public AplicRelDesempenhoTests()
        {
            _mockRepAtualizacaoTarefa = new Mock<IRepAtualizacaoTarefa>();
            _mockRepUsuario = new Mock<IRepUsuario>();
            _aplicRelDesempenho = new AplicRelDesempenho(_mockRepAtualizacaoTarefa.Object, _mockRepUsuario.Object);
        }

        [Fact]
        public async Task GerarMediaTarefasPorUsuario_ReturnsCorrectAverage_WhenSuccessful()
        {
            var loginUsuario = "usuario1";
            var usuario = new Usuario { Login = loginUsuario, Acesso = Acesso.Gerente };
            var atualizacoes = new List<AtualizacaoTarefa>
            {
                new () {
                    Usuario = usuario,
                    Tarefa = new Tarefa(Prioridade.Baixa) {
                        Status = Status.Finalizado
                    },
                    DataAlteracao = DateTime.Now.AddDays(-28)
                },
                new () {
                    Usuario = usuario,
                    Tarefa = new Tarefa (Prioridade.Alta){
                        Status = Status.Finalizado
                    },
                    DataAlteracao = DateTime.Now.AddDays(-32)
                },
                new () {
                    Usuario = usuario,
                    Tarefa = new Tarefa (Prioridade.Media){
                        Status = Status.Pendente
                    },
                    DataAlteracao = DateTime.Now.AddDays(-25) }
                ,
                new () {
                    Usuario = usuario,
                    Tarefa = new Tarefa (Prioridade.Alta){
                        Status = Status.Finalizado
                    },
                    DataAlteracao = DateTime.Now.AddDays(-16)
                }
            };

            _mockRepUsuario.Setup(x => x.GetUserByLogin(loginUsuario)).ReturnsAsync(usuario);
            _mockRepAtualizacaoTarefa.Setup(x => x.FindAllAsync()).ReturnsAsync(atualizacoes);

            var result = await _aplicRelDesempenho.GerarMediaTarefasPorUsuario(loginUsuario);

            Assert.Equal(1.0, result);
        }

        [Fact]
        public async Task GerarMediaTarefasPorUsuario_ThrowsException_WhenUserNotFound()
        {
            var loginUsuario = "usuarioInexistente";
            _mockRepUsuario.Setup(x => x.FindAllAsync()).ReturnsAsync(new List<Usuario>());

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicRelDesempenho.GerarMediaTarefasPorUsuario(loginUsuario));
            Assert.Equal($"Usuário de login {loginUsuario} não encontrado ou sem permissão para gerar o relatório.", exception.Message);
        }

        [Fact]
        public async Task GerarMediaTarefasPorUsuario_ThrowsException_WhenUserIsNotGerente()
        {
            var loginUsuario = "usuario1";
            var usuario = new Usuario { Login = loginUsuario, Acesso = Acesso.Basico };
            _mockRepUsuario.Setup(x => x.FindAllAsync()).ReturnsAsync(new List<Usuario> { usuario });

            var exception = await Assert.ThrowsAsync<Exception>(() => _aplicRelDesempenho.GerarMediaTarefasPorUsuario(loginUsuario));
            Assert.Equal($"Usuário de login {loginUsuario} não encontrado ou sem permissão para gerar o relatório.", exception.Message);
        }
    }
}
