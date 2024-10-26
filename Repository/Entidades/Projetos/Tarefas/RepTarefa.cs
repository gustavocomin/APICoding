using Domain.Projetos.Tarefas;
using Domain.Projetos.Tarefas.Models;
using Repository.Base;
using Repository.Config.Db;

namespace Repository.Entidades.Projetos.Tarefas
{
    public class RepTarefa(DataContext contexto) : RepBase<Tarefa>(contexto), IRepTarefa
    {
    }
}