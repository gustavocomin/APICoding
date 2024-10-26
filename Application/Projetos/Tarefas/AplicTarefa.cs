using Application.Projetos.Tarefas;
using Domain.Projetos;
using Domain.Projetos.Tarefas;
using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Domain.Projetos.Tarefas.Comentarios;
using Domain.Projetos.Tarefas.Comentarios.Models;
using Domain.Projetos.Tarefas.Models;
using Domain.Usuarios;

namespace Application.Tarefas.Tarefas
{
    public class AplicTarefa(IRepTarefa repTarefa,
                             IRepUsuario repUsuario,
                             IRepAtualizacaoTarefa repAtualizacaoTarefa,
                             IRepComentario repComentario,
                             IRepProjeto repProjeto) : IAplicTarefa
    {
        private readonly IRepTarefa _repTarefa = repTarefa;
        private readonly IRepUsuario _repUsuario = repUsuario;
        private readonly IRepAtualizacaoTarefa _repAtualizacaoTarefa = repAtualizacaoTarefa;
        private readonly IRepComentario _repComentario = repComentario;
        private readonly IRepProjeto _repProjeto = repProjeto;

        public async Task<TarefaView> AdicionarTarefa(TarefaDto tarefaDto)
        {
            try
            {
                var tarefa = new Tarefa(tarefaDto.Prioridade)
                {
                    IdProjeto = tarefaDto.IdProjeto,
                    Status = tarefaDto.Status
                };

                var projeto = await _repProjeto.FindByCodigoAsync(tarefaDto.IdProjeto);
                projeto.PodeAdicionarTarefas();

                await _repTarefa.SaveChangesAsync(tarefa);

                var usuario = await _repUsuario.GetUserByLogin(tarefaDto.LoginUsuario);

                await SalvarComentarios(tarefaDto.Comentario, tarefa.Id);
                await SalvarAtualizacoes(tarefa, tarefaDto, usuario);

                var view = new TarefaView(tarefa);

                return view;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TarefaView> AlterarTarefa(int idTarefa, TarefaDto tarefaDto)
        {
            var tarefa = await _repTarefa.FindByCodigoAsync(idTarefa) ?? throw new Exception($"Tarefa de Id: {idTarefa} não encontrado.");
            var usuario = await _repUsuario.GetUserByLogin(tarefaDto.LoginUsuario);

            await SalvarAtualizacoes(tarefa, tarefaDto, usuario);
            await SalvarComentarios(tarefaDto.Comentario, tarefa.IdProjeto);

            tarefa.AtualizaTarefa(tarefaDto);
            await _repTarefa.SaveChangesAsync(tarefa);

            var view = new TarefaView(tarefa);

            return view;
        }

        public async Task<List<TarefaView>> ListarTarefas()
        {
            var tarefas = await _repTarefa.FindAllAsync();
            var view = new TarefaView().MapearTarefas(tarefas);

            return view;
        }

        public async Task<TarefaView> ListarTarefasPorId(int idTarefa)
        {
            var tarefa = await _repTarefa.FindByCodigoAsync(idTarefa) ?? throw new Exception($"Tarefa de Id: {idTarefa} não encontrado.");

            var view = new TarefaView(tarefa);

            return view;
        }

        public async Task RemoverTarefa(int idTarefa, string loginUsuario)
        {
            var usuario = await _repUsuario.GetUserByLogin(loginUsuario);
            var tarefa = await _repTarefa.FindByCodigoAsync(idTarefa) ?? throw new Exception($"Tarefa de Id: {idTarefa} não encontrado.");

            await SalvarAtualizacoes(tarefa, null, usuario);

            await _repTarefa.DeleteByIdAsync(tarefa.Id);
        }

        private async Task SalvarComentarios(ComentarioDto comentarioDtos, int idTarefa)
        {
            if (comentarioDtos == null)
                return;

            var comentario = new Comentario(comentarioDtos, idTarefa);
            await _repComentario.SaveChangesAsync(comentario);
        }

        private async Task SalvarAtualizacoes(Tarefa tarefa, TarefaDto tarefaDto, Usuario usuario)
        {
            var atualizacao = new AtualizacaoTarefa();
            atualizacao.MontarAtualizacao(tarefa, tarefaDto);
            atualizacao.AdicionarTarefa(tarefa, usuario);
            await _repAtualizacaoTarefa.SaveChangesAsync(atualizacao);
        }
    }
}