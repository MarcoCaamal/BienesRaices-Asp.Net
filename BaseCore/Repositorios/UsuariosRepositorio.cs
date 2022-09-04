﻿using BaseCore.Entidades;
using Dapper;
using System.Data.SqlClient;

namespace BaseCore.Repositorios
{
    public interface IUsuariosRepositorio
    {
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }
    public class UsuariosRepositorio: IUsuariosRepositorio
    {
        private readonly string _connectionString;
        public UsuariosRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var conncetion = new SqlConnection(_connectionString);
            var id = await conncetion.QuerySingleAsync<int>(@"
                INSERT INTO Usuarios (Email, EmailNormalizado, PasswordHash)
                VALUES(@Email, @EmailNormalizado, @PasswordHash);", usuario);
            return id;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuarios WHERE EmailNormalizado = @EmailNormalizado", new { emailNormalizado });
        }
    }
}
