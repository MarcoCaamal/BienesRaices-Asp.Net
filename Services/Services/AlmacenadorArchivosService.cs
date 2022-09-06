using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.IServices;

namespace Services.Services
{
    public class AlmacenadorArchivosService : IAlmacenadorArchivosService
    {
        private readonly ILogger<AlmacenadorArchivosService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AlmacenadorArchivosService(ILogger<AlmacenadorArchivosService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<string> GuardarImagen(IFormFile imagen, string contenedor, string rootPath)
        {
            if(imagen == null)
            {
                _logger.LogError("La imagen es nula");
                return "";
            }

            var extension = Path.GetExtension(imagen.FileName);
            var nombreImagen = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(rootPath, contenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(rootPath, contenedor);

            byte[] contenido;

            
            try
            {
                //Obtenemos el contenido de la imagen
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    contenido = memoryStream.ToArray();
                }
                //La guardamos con el redimensinamiento
                using (var image = new MagickImage(contenido))
                {
                    var size = new MagickGeometry(800, 0);

                    image.Resize(size);

                    var rutaEscritura = Path.Combine(ruta, nombreImagen);

                    await image.WriteAsync(rutaEscritura);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return "";
            }

            var urlActual = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var urlParaDB = Path.Combine(urlActual, contenedor, nombreImagen).Replace("\\", "/");
            return urlParaDB;
        }

        public async Task<string> EditarImagen(IFormFile imagen, string ruta, string contendor, string rootPath)
        {
            if (imagen == null)
            {
                _logger.LogError("La imagen es nula");
                return "";
            }

            if(await BorrarImagen(ruta, contendor, rootPath) is false)
            {
                _logger.LogError("Ocurrio un error al borrar la imagen");
                return "";
            }

            var rutaNueva = await GuardarImagen(imagen, contendor, rootPath);

            if (string.IsNullOrEmpty(rutaNueva))
            {
                _logger.LogError("Hubo un fallo al guardar el nuevo archivo");
            }

            return rutaNueva;
        }

        public Task<bool> BorrarImagen(string ruta, string contenedor, string rootPath)
        {
            if(ruta is null)
            {
                _logger.LogError("La ruta para borrar la imagen es nula");
                return Task.FromResult(false);
            }

            var nombreArchivo = Path.GetFileName(ruta);
            string directorio = Path.Combine(rootPath, contenedor, nombreArchivo);

            if (File.Exists(directorio))
            {
                File.Delete(directorio);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
