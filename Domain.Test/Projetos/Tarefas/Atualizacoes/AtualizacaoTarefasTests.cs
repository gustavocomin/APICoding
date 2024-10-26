using Domain.Enums;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Domain.Projetos.Tarefas.Models;
using Domain.Usuarios;

namespace Domain.Test.Projetos.Tarefas.Atualizacoes
{
    public class AtualizacaoTarefasTests
    {
        [Fact]
        public void MontarAtualizacao_StatusAlterado_GeraDescricaoCorreta()
        {
            var tarefaAtual = new Tarefa(Prioridade.Baixa) { Status = Status.Pendente };

            var usuario = new Usuario
            {
                Login = "usuario1"

            };

            var tarefaDto = new TarefaDto()
            {
                Status = Status.Finalizado,
                LoginUsuario = usuario.Login
            };

            var atualizacao = new AtualizacaoTarefa();

            atualizacao.MontarAtualizacao(tarefaAtual, tarefaDto);
            atualizacao.AdicionarTarefa(tarefaAtual, usuario);

            Assert.Contains("Status alterada de: Pendente para: Finalizado", atualizacao.Descricao);
            Assert.Contains("Usuário que fez a alteração: usuario1", atualizacao.Descricao);
        }

        [Fact]
        public void MontarAtualizacao_NovosComentarios_GeraDescricaoCorreta()
        {
            var tarefaAtual = new Tarefa(Prioridade.Baixa)
            {
                Comentarios =
                [
                    new() {
                        Texto = "Comentário 1"
                    },
                    new() {
                        Texto = "Comentário 2"
                    }
                ]
            };

            var usuario = new Usuario
            {
                Login = "usuario1"
            };

            var tarefaDto = new TarefaDto()
            {
                Comentario = new()
                {
                    Texto = "Comentário 3"
                },
                LoginUsuario = usuario.Login
            };


            var atualizacao = new AtualizacaoTarefa();

            atualizacao.MontarAtualizacao(tarefaAtual, tarefaDto);
            atualizacao.AdicionarTarefa(tarefaAtual, usuario);

            Assert.Contains("Novo comentário: Comentário 3", atualizacao.Descricao);
            Assert.Contains("Usuário que fez a alteração: usuario1", atualizacao.Descricao);
        }

        [Fact]
        public void MontarAtualizacao_SemAlteracoes_NaoGeraDescricao()
        {
            var tarefaAtual = new Tarefa(Prioridade.Baixa)
            {
                Status = Status.Finalizado
            };
            var tarefaDto = new TarefaDto()
            {
                Status = Status.Finalizado
            };

            var usuario = new Usuario { Login = "usuario1" };
            var atualizacao = new AtualizacaoTarefa();

            atualizacao.MontarAtualizacao(tarefaAtual, tarefaDto);
            atualizacao.AdicionarTarefa(tarefaAtual, usuario);

            Assert.Empty(atualizacao.Descricao);
        }
    }
}