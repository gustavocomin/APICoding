using Domain.Projetos.Models;

namespace Application.Projetos
{
    public interface IAplicProjeto
    {
        Task<ProjetoView> AdicionarProjeto(ProjetoDto projetoDto);
        Task<ProjetoView> AlterarProjeto(int idProjeto, ProjetoDto projetoDto);
        Task<List<ProjetoView>> ListarProjetos();
        Task<ProjetoView> ListarProjetosPorId(int idProjeto);
        Task RemoverProjeto(int idProjeto);
    }
}