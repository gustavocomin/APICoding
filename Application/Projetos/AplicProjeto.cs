using Domain.Projetos;
using Domain.Projetos.Models;

namespace Application.Projetos
{
    public class AplicProjeto(IRepProjeto repProjeto) : IAplicProjeto
    {
        private readonly IRepProjeto _repProjeto = repProjeto;

        public async Task<ProjetoView> AdicionarProjeto(ProjetoDto projetoDto)
        {
            var projeto = new Projeto()
            {
                Descricao = projetoDto.Descricao
            };

            await _repProjeto.SaveChangesAsync(projeto);
            var view = new ProjetoView(projeto);

            return view;
        }

        public async Task<ProjetoView> AlterarProjeto(int idProjeto, ProjetoDto projetoDto)
        {
            var projeto = await _repProjeto.FindByCodigoAsync(idProjeto) ?? throw new Exception($"Projeto de Id: {idProjeto} não encontrado.");
            projeto.Descricao = projetoDto.Descricao;
            await _repProjeto.SaveChangesAsync(projeto);

            var view = new ProjetoView(projeto);

            return view;
        }

        public async Task<List<ProjetoView>> ListarProjetos()
        {
            var projetos = await _repProjeto.FindAllAsync();
            var view = new ProjetoView().MapearProjetos(projetos);
            return view;
        }

        public async Task<ProjetoView> ListarProjetosPorId(int idProjeto)
        {
            var projeto = await _repProjeto.FindByCodigoAsync(idProjeto) ?? throw new Exception($"Projeto de Id: {idProjeto} não encontrado.");
            var view = new ProjetoView(projeto);
            return view;
        }

        public async Task RemoverProjeto(int idProjeto)
        {
            var projeto = await _repProjeto.FindByCodigoAsync(idProjeto) ?? throw new Exception($"Projeto de Id: {idProjeto} não encontrado.");

            if (projeto.PodeRemoverProjeto())
                await _repProjeto.DeleteByIdAsync(projeto.Id);
        }
    }
}