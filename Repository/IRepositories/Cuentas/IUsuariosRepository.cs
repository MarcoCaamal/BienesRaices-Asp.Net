using Domain.Entidades.Cuentas;

namespace Repository.IRepositories.Cuentas
{
    public interface IUsuariosRepository
    {
        Task<Usuario?> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }
}
