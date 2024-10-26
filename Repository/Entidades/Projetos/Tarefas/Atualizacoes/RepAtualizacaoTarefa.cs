using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Repository.Base;
using Repository.Config.Db;

namespace Repository.Entidades.Projetos.Tarefas.Atualizacoes
{
    public class RepAtualizacaoTarefa(DataContext contexto) : RepBase<AtualizacaoTarefa>(contexto), IRepAtualizacaoTarefa
    {
    }
}