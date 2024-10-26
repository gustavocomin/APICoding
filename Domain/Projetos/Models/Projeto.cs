using Domain.Base;
using Domain.Enums;
using Domain.Projetos.Tarefas.Models;

namespace Domain.Projetos.Models
{
    public class Projeto : IdBase
    {
        public string Descricao { get; set; } = "";
        public DateTime DataCriacao { get; set; }

        public List<Tarefa> Tarefas { get; set; }

        public Projeto()
        {
            DataCriacao = DateTime.Now;
        }


        public bool PodeAdicionarTarefas()
        {
            if (Tarefas?.Count == 20)
                throw new ArgumentException("Um projeto só pode ter até 20 tarefas.");

            return true;
        }


        public bool PodeRemoverProjeto()
        {
            if (Tarefas.Any(x => x.Status == Status.Pendente))
                throw new Exception("Projeto possui tarefas pentendes. Para removê-lo, é necessário concluir ou remover as tarefas pendentes.");

            return true;
        }
    }
}