using BaseCore.Entidades.ViewModels;
using Domain.Entidades.Propiedades;
using Domain.Entidades.Propiedades.ViewModels;
using Domain.Util;

namespace Services.IServices.Propiedades
{
    public interface IPropiedadesService
    {
        Task<ResponseHelper> Crear(PropiedadCreacionVM model, string rootPath);
        Task<ResponseHelper> Editar(PropiedadCreacionVM model, string rootPath);
        Task<ResponseHelper> Eliminar(Propiedad propiedad, string rootPath);
        Task<IEnumerable<Propiedad>?> ObtenerLista();
        Task<IEnumerable<AnunciosVM>?> ObtenerListaParaAnuncios(int? numeroRegistros);
        Task<IEnumerable<AnunciosVM>?> ObtenerListaParaAnuncios();
        Task<IEnumerable<IndexPropiedadesVM>?> ObtenerListaParaIndex();
        Task<Propiedad?> ObtenerPorId(int? id);
    }
}
