using Domain.Entidades.Vendedores;

namespace Repository.IRepositories.Vendedores
{
    public interface IVendedoresRepository
    {
        Task<int> Crear(Vendedor vendedor);
        Task<int> Editar(Vendedor vendedor);
        Task<int> Eliminar(int? id);
        Task<IEnumerable<Vendedor>> ObtenerLista();
        Task<Vendedor> ObtenerPorId(int? id);
    }
}
