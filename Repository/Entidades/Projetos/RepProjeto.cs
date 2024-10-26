using Domain.Projetos;
using Domain.Projetos.Models;
using Repository.Base;
using Repository.Config.Db;

namespace Repository.Entidades.Projetos
{
    public class RepProjeto(DataContext contexto) : RepBase<Projeto>(contexto), IRepProjeto
    {
    }
}