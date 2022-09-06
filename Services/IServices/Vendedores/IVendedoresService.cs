using Domain.Entidades.Vendedores;
using Domain.Util;

namespace Services.IServices.Vendedores
{
    public interface IVendedoresService
    {
        Task<ResponseHelper> Crear(Vendedor model);
        Task<ResponseHelper> Editar(Vendedor model);
        Task<ResponseHelper> Eliminar(int? id);
        Task<IEnumerable<Vendedor>> ObtenerLista();
        Task<Vendedor> ObtenerPorId(int? id);
    }
}
