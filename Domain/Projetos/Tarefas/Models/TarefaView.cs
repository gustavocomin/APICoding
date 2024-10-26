using Domain.Base;
using Domain.Enums;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Domain.Projetos.Tarefas.Comentarios.Models;

namespace Domain.Projetos.Tarefas.Models
{
    public class TarefaView : IdBase
    {
        public Prioridade Prioridade { get; set; }
        public Status Status { get; set; }

        public int IdProjeto { get; set; }

        public List<ComentarioView>? Comentarios { get; set; }
        public List<AtualizacaoTarefaView>? AtualizacaoTarefa { get; set; }

        public TarefaView()
        {
        }

        public TarefaView(Tarefa tarefa)
        {
            Id = tarefa.Id;
            Prioridade = tarefa.Prioridade;
            Status = tarefa.Status;
            IdProjeto = tarefa.IdProjeto;
            Comentarios = new ComentarioView().MapearComentarios(tarefa.Comentarios);
            AtualizacaoTarefa = new AtualizacaoTarefaView().MapearAtualizacoes(tarefa.AtualizacaoTarefa);
        }

        public List<TarefaView> MapearTarefas(List<Tarefa> Tarefas)
        {
            var list = new List<TarefaView>();
            if (Tarefas == null || Tarefas.Count == 0)
                return list;

            foreach (var tarefa in Tarefas)
            {
                list.Add(new TarefaView(tarefa));
            }

            return list;
        }
    }
}