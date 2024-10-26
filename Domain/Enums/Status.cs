using System.ComponentModel;

namespace Domain.Enums
{
    public enum Status
    {
        [Description("Ativo")]
        Ativo = 0,
        [Description("Pendente")]
        Pendente = 1,
        [Description("Finalizado")]
        Finalizado = 2,
    }
}
