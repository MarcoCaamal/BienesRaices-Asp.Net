using BaseCore.Entidades;
using BaseCore.Entidades.ViewModels;
using BaseCore.Repositorios;
using BaseCore.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseCore.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IVendedoresRepositorio _vendedoresRepositorio;
        private readonly IAlmacenadorDeArchivos _almacenadorDeArchivos;
        private readonly IPropiedadesRepositorio _propiedadesRepositorio;
        private readonly string _carpetaContenedora = "propiedades";

        public AdminController(IVendedoresRepositorio vendedoresRepositorio,
            IAlmacenadorDeArchivos almacenadorDeArchivos,
            IPropiedadesRepositorio propiedadesRepositorio)
        {
            _vendedoresRepositorio = vendedoresRepositorio;
            _almacenadorDeArchivos = almacenadorDeArchivos;
            _propiedadesRepositorio = propiedadesRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _propiedadesRepositorio.ObtenerListaIndex();
            return View(model);
        }

        [Route("Propiedades/[Action]")]
        public async Task<IActionResult> Crear()
        {
            var model = new PropiedadCreacionVM();
            model.Vendedores = await ObtenerVendedores();
            return View(model);
        }

        [HttpPost]
        [Route("Propiedades/[Action]")]
        public async Task<IActionResult> Crear(PropiedadCreacionVM model)
        {
            if (model.ImagenPropiedad is null)
            {
                ModelState.AddModelError(nameof(model.ImagenPropiedad), "La imagen es requerida.");
            }

            if (!ModelState.IsValid)
            {
                model.Vendedores = await ObtenerVendedores();
                return View(model);
            }

            var propiedad = new Propiedad()
            {
                Titulo = model.Propiedad.Titulo,
                Precio = model.Propiedad.Precio,
                Descripcion = model.Propiedad.Descripcion,
                Habitaciones = model.Propiedad.Habitaciones,
                Wc = model.Propiedad.Wc,
                Estacionamientos = model.Propiedad.Estacionamientos,
                VendedorId = model.Propiedad.VendedorId,
                Creado = DateTime.Now
            };


            using (var memoryStream = new MemoryStream())
            {
                await model.ImagenPropiedad.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray();
                var extension = Path.GetExtension(model.ImagenPropiedad.FileName);
                propiedad.Imagen = await _almacenadorDeArchivos.GuardarArchivo(contenido, extension, _carpetaContenedora,
                    model.ImagenPropiedad.ContentType);
            }

            await _propiedadesRepositorio.Crear(propiedad);

            TempData["Success"] = "La propiedad se correctamente";

            return RedirectToAction("Index");
        }

        [Route("Propiedades/[Action]")]
        public async Task<IActionResult> Editar(int? id)
        {
            if(id is null)
            {
                return RedirectToAction("Index");
            }

            var propiedad = await _propiedadesRepositorio.ObtenerPorId(id);

            if (propiedad is null)
            {
                TempData["Error"] = "La propiedad no existe.";
                return RedirectToAction("Index");
            }

            var model = new PropiedadCreacionVM();
            model.Propiedad = propiedad;
            model.Vendedores = await ObtenerVendedores();
            return View(model);
        }

        [HttpPost]
        [Route("Propiedades/[Action]")]
        public async Task<IActionResult> Editar(PropiedadCreacionVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Vendedores = await ObtenerVendedores();
                return View(model);
            }

            var vendedor = await _vendedoresRepositorio.ObtenerPorId(model.Propiedad.VendedorId);

            if(vendedor is null)
            {
                TempData["Error"] = "El Vendedor no existe";
                return RedirectToAction("Index");
            }

            var propiedad = new Propiedad()
            {
                Id = model.Propiedad.Id,
                Titulo = model.Propiedad.Titulo,
                Precio = model.Propiedad.Precio,
                Imagen = model.Propiedad.Imagen,
                Descripcion = model.Propiedad.Descripcion,
                Habitaciones = model.Propiedad.Habitaciones,
                Wc = model.Propiedad.Wc,
                Estacionamientos = model.Propiedad.Estacionamientos,
                VendedorId = model.Propiedad.VendedorId
            };

            if (model.ImagenPropiedad != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImagenPropiedad.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(model.ImagenPropiedad.FileName);
                    propiedad.Imagen = await _almacenadorDeArchivos.EditarArchivo(contenido, extension, _carpetaContenedora,
                        model.Propiedad.Imagen ,model.ImagenPropiedad.ContentType);
                }
            }

            await _propiedadesRepositorio.Editar(propiedad);

            TempData["Success"] = "La propiedad se actualizo correctamente.";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            var propiedad = await _propiedadesRepositorio.ObtenerPorId(id);

            if(propiedad is null)
            {
                TempData["Error"] = $"La propiedad con el ID {propiedad.Id} no fue encontrada.";
                return RedirectToAction("Index");
            }

            await _almacenadorDeArchivos.BorrarArchivo(propiedad.Imagen, _carpetaContenedora);

            await _propiedadesRepositorio.Eliminar(id);
            TempData["Success"] = "La propiedad se elimino correctamente.";

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerVendedores()
        {
            var vendedores = await _vendedoresRepositorio.ObtenerLista();
            return vendedores.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = $"{x.Nombre} {x.Apellido}"
            });
        }
    }
}
