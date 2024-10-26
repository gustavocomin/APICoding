using Domain.Base;

namespace Domain.Projetos.Tarefas.Comentarios.Models
{
    public class ComentarioView : IdBase
    {
        public string Texto { get; set; } = "";

        public int IdTarefa { get; set; }

        public ComentarioView()
        {
        }

        public ComentarioView(Comentario comentario)
        {
            Texto = comentario.Texto;
            IdTarefa = comentario.IdTarefa;
            Id = comentario.Id;
        }

        public List<ComentarioView> MapearComentarios(List<Comentario> comentarios)
        {
            var list = new List<ComentarioView>();
            if (comentarios == null || comentarios.Count == 0)
                return list;

            foreach (var comentario in comentarios)
            {
                list.Add(new ComentarioView(comentario));
            }

            return list;
        }
    }
}
