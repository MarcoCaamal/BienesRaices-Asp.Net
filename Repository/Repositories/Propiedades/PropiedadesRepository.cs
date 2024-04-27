using BaseCore.Entidades.ViewModels;
using Dapper;
using Domain.Entidades.Propiedades;
using Microsoft.Extensions.Configuration;
using Repository.IRepositories.Propiedades;
using System.Data.SqlClient;

namespace Repository.Repositories.Propiedades
{
    public class PropiedadesRepository : IPropiedadesRepository
    {
        private readonly string _connectionString;
        public PropiedadesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Propiedad>> ObtenerLista()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Propiedad>(@"SELECT * FROM Propiedades");
        }

        public async Task<IEnumerable<Propiedad>> ObtenerLista(int? numeroRegistros)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Propiedad>(@$"SELECT TOP({numeroRegistros}) * FROM Propiedades");
        }

        public async Task<Propiedad?> ObtenerPorId(int? id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Propiedad?>(
                @"SELECT * FROM Propiedades WHERE Id = @Id", new { id });
        }

        public async Task<int> Crear(Propiedad propiedad)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Propiedades (Titulo, Precio, Imagen, Descripcion, 
                Habitaciones, Wc, Estacionamiento, Creado, VendedorId)
                VALUES(@Titulo, @Precio, @Imagen, @Descripcion, @Habitaciones, @Wc,
                @Estacionamiento, @Creado, @VendedorId)
                SELECT SCOPE_IDENTITY();", propiedad);
            propiedad.Id = id;
            return id;
        }

        public async Task<int> Editar(Propiedad propiedad)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                @"UPDATE Propiedades
                SET Titulo = @Titulo, Precio = @Precio, Imagen = @Imagen, Descripcion = @Descripcion,
                Habitaciones = @Habitaciones, Wc = @Wc, Estacionamiento = @Estacionamiento, VendedorId = @VendedorId
                WHERE Id = @Id;", propiedad);
        }

        public async Task<int> Eliminar(int? id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE Propiedades WHERE Id = @Id", new { id });
        }
    }
}
