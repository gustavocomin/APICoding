using Domain.Projetos.Tarefas.Models;

namespace Application.Projetos.Tarefas
{
    public interface IAplicTarefa
    {
        Task<TarefaView> AdicionarTarefa(TarefaDto tarefaDto);
        Task<TarefaView> AlterarTarefa(int idTarefa, TarefaDto tarefaDto);
        Task<List<TarefaView>> ListarTarefas();
        Task<TarefaView> ListarTarefasPorId(int idTarefa);
        Task RemoverTarefa(int idTarefa, string loginUsuario);
    }
}