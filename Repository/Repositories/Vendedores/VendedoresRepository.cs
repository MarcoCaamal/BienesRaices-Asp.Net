using Dapper;
using Domain.Entidades.Vendedores;
using Microsoft.Extensions.Configuration;
using Repository.IRepositories.Vendedores;
using System.Data.SqlClient;

namespace Repository.Repositories.Vendedores
{
    public class VendedoresRepository : IVendedoresRepository
    {
        private readonly string _connectionString;
        public VendedoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Vendedor?> ObtenerPorId(int? id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Vendedor>("SELECT * FROM Vendedores WHERE Id = @Id", new { id });
        }

        public async Task<IEnumerable<Vendedor>> ObtenerLista()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Vendedor>(
                @"SELECT Id, Nombre, Apellido, Telefono
                FROM Vendedores;");
        }

        public async Task<int> Crear(Vendedor vendedor)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Vendedores(Nombre, Apellido, Telefono)
                VALUES(@Nombre, @Apellido, @Telefono)
                SELECT SCOPE_IDENTITY();", vendedor);
            vendedor.Id = id;
            return id;
        }

        public async Task<int> Editar(Vendedor vendedor)
        {
            using var conncetion = new SqlConnection(_connectionString);
            return await conncetion.ExecuteAsync(
                @"UPDATE Vendedores
                SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono
                WHERE Id = @Id;", vendedor);
        }

        public async Task<int> Eliminar(int? id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE Vendedores WHERE Id = @Id", new { id });
        }
    }
}
