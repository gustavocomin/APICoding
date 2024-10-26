namespace Application.Relatorios.Desempenho
{
    public interface IAplicRelDesempenho
    {
        Task<double> GerarMediaTarefasPorUsuario(string loginUsuario);
    }
}