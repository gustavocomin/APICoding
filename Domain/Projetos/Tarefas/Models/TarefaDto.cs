using Domain.Enums;
using Domain.Projetos.Tarefas.Comentarios.Models;

namespace Domain.Projetos.Tarefas.Models
{
    public class TarefaDto
    {
        public Prioridade Prioridade { get; set; }
        public string LoginUsuario { get; set; } = "";
        public int IdProjeto { get; set; }
        public Status Status { get; set; }
        public ComentarioDto? Comentario { get; set; }
    }
}