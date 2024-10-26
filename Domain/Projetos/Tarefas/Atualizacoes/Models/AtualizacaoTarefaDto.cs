using Domain.Base;

namespace Domain.Projetos.Tarefas.Atualizacoes.Models
{
    public class AtualizacaoTarefaDto : IdBase
    {
        public string Descricao { get; set; } = "";
        public DateTime DataAlteracao { get; set; }

        public int CodigoUsuario { get; set; }

        public int IdTarefa { get; set; }
    }
}