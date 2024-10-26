using Domain.Base;
using Domain.Projetos.Tarefas.Models;

namespace Domain.Projetos.Tarefas.Comentarios.Models
{
    public class Comentario : IdBase
    {
        public int IdComentario { get; set; }
        public string Texto { get; set; } = "";

        public int IdTarefa { get; set; }
        public Tarefa Tarefa { get; set; }

        public Comentario()
        {
        }

        public Comentario(ComentarioDto comentarioDto, int idTarefa)
        {
            Texto = comentarioDto.Texto;
            IdTarefa = idTarefa;
        }
    }
}