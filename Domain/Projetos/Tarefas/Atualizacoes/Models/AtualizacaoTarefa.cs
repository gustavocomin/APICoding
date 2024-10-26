using Domain.Base;
using Domain.Projetos.Tarefas.Models;
using Domain.Usuarios;
using System.Text;

namespace Domain.Projetos.Tarefas.Atualizacoes.Models
{
    public class AtualizacaoTarefa : IdBase
    {
        public string Descricao { get; set; } = "";
        public DateTime DataAlteracao { get; set; }

        public int CodigoUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public int IdTarefa { get; set; }
        public Tarefa Tarefa { get; set; }

        public AtualizacaoTarefa()
        {
        }

        public AtualizacaoTarefa(AtualizacaoTarefaDto atualizacaoTarefa)
        {
            Descricao = atualizacaoTarefa.Descricao;
            DataAlteracao = atualizacaoTarefa.DataAlteracao;
            CodigoUsuario = atualizacaoTarefa.CodigoUsuario;
            IdTarefa = atualizacaoTarefa.IdTarefa;
        }

        public List<AtualizacaoTarefa> MontarAtualizacaoTarefas(List<AtualizacaoTarefaDto> atualizacaoTarefas)
        {
            var list = new List<AtualizacaoTarefa>();
            if (atualizacaoTarefas == null || atualizacaoTarefas.Count == 0)
                return list;

            foreach (var atualizacaoTarefa in atualizacaoTarefas)
            {
                list.Add(new AtualizacaoTarefa(atualizacaoTarefa));
            }

            return list;
        }

        public void MontarAtualizacao(Tarefa tarefa, TarefaDto tarefaDto)
        {
            bool temAlteracao = false;
            var sb = new StringBuilder();

            if (tarefa == null && tarefaDto == null)
                return;

            if (tarefa == null && tarefaDto == null)
            {
                sb.AppendLine($"Tarefa de Id: {tarefa!.Id} Removida.");
                temAlteracao = true;
            }

            if (tarefa != null && tarefaDto != null)
            {
                if (tarefa.Status != tarefaDto.Status)
                {
                    sb.AppendLine($"Status alterada de: {tarefa.Status} para: {tarefaDto.Status}");
                    temAlteracao = true;
                }
            }

            if (tarefa == null)
            {
                sb.AppendLine($"Tarefa criada com prioridade: {tarefaDto!.Prioridade}");
                sb.AppendLine($"Status: {tarefaDto.Status}");
            }

            if (tarefaDto?.Comentario != null)
            {
                sb.AppendLine($"Novo comentário: {tarefaDto.Comentario.Texto}");
                temAlteracao = true;
            }

            if (temAlteracao)
                sb.AppendLine($"Usuário que fez a alteração: {tarefaDto.LoginUsuario}");

            Descricao = sb.ToString();
            DataAlteracao = DateTime.Now;
        }

        public void AdicionarTarefa(Tarefa tarefa, Usuario usuario)
        {
            Tarefa = tarefa;
            IdTarefa = tarefa.Id;
            Usuario = usuario;

            if (!string.IsNullOrWhiteSpace(Descricao))
                Descricao += $"\r\nTarefa de Id: {IdTarefa}";
        }
    }
}