using Domain.Base;
using Domain.Enums;
using Domain.Projetos.Models;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Domain.Projetos.Tarefas.Comentarios.Models;

namespace Domain.Projetos.Tarefas.Models
{
    public class Tarefa : IdBase
    {

        public Tarefa()
        {
        }

        public Tarefa(Prioridade prioridade)
        {
            Prioridade = prioridade;
        }

        public Prioridade Prioridade { get; private set; }
        public Status Status { get; set; }

        public int IdProjeto { get; set; }
        public Projeto Projeto { get; set; }

        public List<Comentario>? Comentarios { get; set; }
        public List<AtualizacaoTarefa>? AtualizacaoTarefa { get; set; }


        public void AtualizaTarefa(TarefaDto tarefaDto)
        {
            Status = tarefaDto.Status;
            IdProjeto = tarefaDto.IdProjeto;
        }
    }
}