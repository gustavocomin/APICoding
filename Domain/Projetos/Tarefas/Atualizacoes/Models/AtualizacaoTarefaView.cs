using Domain.Base;

namespace Domain.Projetos.Tarefas.Atualizacoes.Models
{
    public class AtualizacaoTarefaView : IdBase
    {
        public string Descricao { get; set; } = "";
        public DateTime DataAlteracao { get; set; }

        public int CodigoUsuario { get; set; }

        public int IdTarefa { get; set; }

        public AtualizacaoTarefaView()
        {
        }

        public AtualizacaoTarefaView(AtualizacaoTarefa atualizacaoTarefa)
        {
            Descricao = atualizacaoTarefa.Descricao;
            DataAlteracao = atualizacaoTarefa.DataAlteracao;
            CodigoUsuario = atualizacaoTarefa.CodigoUsuario;
            IdTarefa = atualizacaoTarefa.IdTarefa;
            Id = atualizacaoTarefa.Id;
        }

        public List<AtualizacaoTarefaView> MapearAtualizacoes(List<AtualizacaoTarefa> atualizacaoTarefas)
        {
            var list = new List<AtualizacaoTarefaView>();
            if (atualizacaoTarefas == null || atualizacaoTarefas.Count == 0)
                return list;

            foreach (var atualizacaoTarefa in atualizacaoTarefas)
            {
                list.Add(new AtualizacaoTarefaView(atualizacaoTarefa));
            }

            return list;
        }
    }
}
