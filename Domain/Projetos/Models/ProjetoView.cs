using Domain.Base;
using Domain.Projetos.Tarefas.Models;

namespace Domain.Projetos.Models
{
    public class ProjetoView : IdBase
    {
        public string Descricao { get; set; } = "";
        public DateTime DataCriacao { get; set; }

        public List<TarefaView> Tarefas { get; set; }

        public ProjetoView()
        {
        }

        public ProjetoView(Projeto projeto)
        {
            Id = projeto.Id;
            Descricao = projeto.Descricao;
            DataCriacao = projeto.DataCriacao;
            Tarefas = new TarefaView().MapearTarefas(projeto.Tarefas);
        }

        public List<ProjetoView> MapearProjetos(List<Projeto> projetos)
        {
            var list = new List<ProjetoView>();
            if (projetos == null || projetos.Count == 0)
                return list;

            foreach (var projeto in projetos)
            {
                list.Add(new ProjetoView(projeto));
            }

            return list; ;
        }
    }
}