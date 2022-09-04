namespace BaseCore.Servicios
{
    public interface IAlmacenadorDeArchivos
    {
        Task<string> EditarArchivo(byte[] contenido, string extension, string carpetaContendora, string ruta, string contentType);
        Task BorrarArchivo(string ruta, string carpetaContendora);
        Task<string> GuardarArchivo(byte[] contenido, string extension, string carpetaContendora, string contentType);
    }

    public class AlmacenadorDeArchivos : IAlmacenadorDeArchivos
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AlmacenadorDeArchivos(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task BorrarArchivo(string ruta, string carpetaContendora)
        {
            if(ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorio = Path.Combine(_env.WebRootPath, carpetaContendora, nombreArchivo);

                if (File.Exists(directorio))
                {
                    File.Delete(directorio);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string carpetaContendora, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, carpetaContendora);
            return await GuardarArchivo(contenido, extension, carpetaContendora, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string carpetaContendora, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string carpeta = Path.Combine(_env.WebRootPath, carpetaContendora);

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            string ruta = Path.Combine(carpeta, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);

            var urlActual = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var urlParaDB = Path.Combine(urlActual, carpetaContendora, nombreArchivo).Replace("\\", "/");
            return urlParaDB;
        }
    }
}
