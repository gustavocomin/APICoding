using Domain.Base;

namespace Domain.Usuarios
{
    public interface IRepUsuario : IRepBase<Usuario>
    {
        Task<Usuario> GetUserByLogin(string login);
    }
}