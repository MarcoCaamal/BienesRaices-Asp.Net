using Microsoft.AspNetCore.Http;

namespace Services.IServices
{
    public interface IAlmacenadorArchivosService
    {
        Task<bool> BorrarImagen(string ruta, string contenedor, string rootPath);
        Task<string> EditarImagen(IFormFile imagen, string ruta, string contendor, string rootPath);
        Task<string> GuardarImagen(IFormFile imagen, string contendor, string rootPath);
    }
}
