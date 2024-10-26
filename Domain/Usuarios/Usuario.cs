using Domain.Base;
using Domain.Enums;

namespace Domain.Usuarios
{
    public class Usuario : IdBase
    {
        public string Nome { get; set; } = "";
        public string Login { get; set; } = "";

        public Acesso Acesso { get; set; }
    }
}