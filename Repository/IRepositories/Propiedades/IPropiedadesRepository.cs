using BaseCore.Entidades.ViewModels;
using Domain.Entidades.Propiedades;

namespace Repository.IRepositories.Propiedades
{
    public interface IPropiedadesRepository
    {
        Task<int> Crear(Propiedad propiedad);
        Task<int> Editar(Propiedad propiedad);
        Task<int> Eliminar(int? id);
        Task<IEnumerable<Propiedad>> ObtenerLista();
        Task<IEnumerable<Propiedad>> ObtenerLista(int? numeroRegistros);
        Task<Propiedad> ObtenerPorId(int? id);
    }
}
