using Dapper;
using Domain.Entidades.Cuentas;
using Microsoft.Extensions.Configuration;
using Repository.IRepositories.Cuentas;
using System.Data.SqlClient;

namespace Repository.Repositories.Cuentas
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly string _connectionString;
        public UsuariosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var conncetion = new SqlConnection(_connectionString);
            var id = await conncetion.QuerySingleAsync<int>(@"
                INSERT INTO Usuarios (Email, EmailNormalizado, PasswordHash)
                VALUES(@Email, @EmailNormalizado, @PasswordHash)
                SELECT SCOPE_IDENTITY();", usuario);
            return id;
        }

        public async Task<Usuario?> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuarios WHERE EmailNormalizado = @EmailNormalizado", new { emailNormalizado });
        }
    }
}
