using Domain.Projetos.Tarefas.Comentarios;
using Domain.Projetos.Tarefas.Comentarios.Models;
using Repository.Base;
using Repository.Config.Db;

namespace Repository.Entidades.Projetos.Tarefas.Comentarios
{
    public class RepComentario(DataContext contexto) : RepBase<Comentario>(contexto), IRepComentario
    {
    }
}