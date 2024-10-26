using Domain.Enums;
using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Usuarios;

namespace Application.Relatorios.Desempenho
{
    public class AplicRelDesempenho(IRepAtualizacaoTarefa repAtualizacaoTarefa, IRepUsuario repUsuario) : IAplicRelDesempenho
    {
        private readonly IRepUsuario _repUsuario = repUsuario;
        private readonly IRepAtualizacaoTarefa _repAtualizacaoTarefa = repAtualizacaoTarefa;

        public async Task<double> GerarMediaTarefasPorUsuario(string loginUsuario)
        {
            var usuario = await _repUsuario.GetUserByLogin(loginUsuario);
            if (usuario == null || usuario.Acesso != Acesso.Gerente)
                throw new Exception($"Usuário de login {loginUsuario} não encontrado ou sem permissão para gerar o relatório.");

            var atualizacoes = await _repAtualizacaoTarefa.FindAllAsync();
            atualizacoes = atualizacoes.Where(x => x.Tarefa.Status == Status.Finalizado &&
                                                   x.DataAlteracao <= DateTime.Today.AddDays(-30)).ToList();
            var usuarios = atualizacoes.Select(x => x.Usuario).Count();
            var tarefasFinalizadas = atualizacoes.Select(x => x.Tarefa).Count();

            if (tarefasFinalizadas == 0)
                return 0;

            return tarefasFinalizadas / usuarios;
        }
    }
}