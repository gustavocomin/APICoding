using System.ComponentModel;

namespace Domain.Enums
{
    public enum Prioridade
    {
        [Description("Baixa")]
        Baixa = 0,
        [Description("Média")]
        Media = 1,
        [Description("Alta")]
        Alta = 2,
    }
}