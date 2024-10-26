using System.ComponentModel;

namespace Domain.Enums
{
    public enum Acesso
    {
        [Description("Básico")]
        Basico = 0,
        [Description("Gerente")]
        Gerente = 1,
    }
}
