using BaseCore.Entidades;
using Dapper;
using System.Data.SqlClient;

namespace BaseCore.Repositorios
{
    public interface IVendedoresRepositorio
    {
        Task<IEnumerable<Vendedor>> ObtenerLista();
        Task<Vendedor> ObtenerPorId(int? id);
    }

    public class VendedoresRepositorio: IVendedoresRepositorio
    {
        private readonly string _connectionString;
        public VendedoresRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Vendedor> ObtenerPorId(int? id)
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
    }
}
