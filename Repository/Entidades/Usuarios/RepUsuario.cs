using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Config.Db;

namespace Repository.Entidades.Usuarios
{
    public class RepUsuario(DataContext contexto) : RepBase<Usuario>(contexto), IRepUsuario
    {
        public async Task<Usuario> GetUserByLogin(string login)
        {
            var usuario = await contexto.Set<Usuario>()
                .Where(x => x.Login.ToLower() == login.ToLower())
                .FirstOrDefaultAsync()
                ?? throw new Exception($"Usuário de login {login} não encontrado.");

            return usuario;
        }
    }
}