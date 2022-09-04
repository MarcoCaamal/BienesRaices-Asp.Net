using BaseCore.Entidades;
using BaseCore.Entidades.ViewModels;
using Dapper;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace BaseCore.Repositorios
{
    public interface IPropiedadesRepositorio
    {
        Task Crear(Propiedad propiedad);
        Task Editar(Propiedad propiedad);
        Task Eliminar(int? id);
        Task<IEnumerable<AnunciosVM>> ObtenerListaAnuncios(int? limite);
        Task<IEnumerable<AnunciosVM>> ObtenerListaAnuncios();
        Task<IEnumerable<IndexPropiedadesVM>> ObtenerListaIndex();
        Task<Propiedad> ObtenerPorId(int? id);
    }

    public class PropiedadesRepositorio : IPropiedadesRepositorio
    {
        private readonly string _connectionString;
        public PropiedadesRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<IndexPropiedadesVM>> ObtenerListaIndex()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<IndexPropiedadesVM>("SELECT Id, Titulo, Imagen, Precio FROM Propiedades;");
        }

        public async Task<IEnumerable<AnunciosVM>> ObtenerListaAnuncios(int? limite)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<AnunciosVM>(
            @$"SELECT TOP({limite}) Id, Titulo, Precio, 
            Imagen, Descripcion, Estacionamiento AS Estacionamientos, Wc, Habitaciones, VendedorId 
            FROM Propiedades;");

        }

        public async Task<IEnumerable<AnunciosVM>> ObtenerListaAnuncios()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<AnunciosVM>(
            @"SELECT Id, Titulo, Precio, 
            Imagen, Descripcion, Estacionamiento AS Estacionamientos, Wc, Habitaciones, VendedorId 
            FROM Propiedades;");

        }

        public async Task<Propiedad> ObtenerPorId(int? id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Propiedad>(
                @"SELECT Id, Titulo, Precio, Imagen, Descripcion, Estacionamiento AS Estacionamientos, Wc, Habitaciones, VendedorId 
                FROM Propiedades 
                WHERE Id = @Id;", new { id });
        }
        public async Task Crear(Propiedad propiedad)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Propiedades(Titulo, Precio, Imagen, Descripcion, Habitaciones, Wc, Estacionamiento, Creado, VendedorId)
                VALUES (@Titulo, @Precio, @Imagen, @Descripcion, @Habitaciones, @Wc, @Estacionamientos, @Creado, @VendedorId)
                SELECT SCOPE_IDENTITY();", propiedad);
            propiedad.Id = id;
        }

        public async Task Editar(Propiedad propiedad)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                @"UPDATE Propiedades
                SET Titulo = @Titulo, Precio = @Precio, Imagen = @Imagen, Descripcion = @Descripcion, 
                Habitaciones = @Habitaciones, Wc = @Wc, Estacionamiento = @Estacionamientos, VendedorId = @VendedorId
                WHERE Id = @Id;", propiedad);
        }

        public async Task Eliminar(int? id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync("DELETE Propiedades WHERE Id = @Id", new { id });
        }
    }
}
