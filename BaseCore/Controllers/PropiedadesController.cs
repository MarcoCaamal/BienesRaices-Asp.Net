using BaseCore.Entidades.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.IServices.Propiedades;
using Services.IServices.Vendedores;

namespace BaseCore.Controllers
{
    public class PropiedadesController : Controller
    {
        
        private readonly IPropiedadesService _propiedadesService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IWebHostEnvironment _env;

        public PropiedadesController(IPropiedadesService propiedadesService,
            IVendedoresService vendedoresService,
            IWebHostEnvironment env)
        {
            _propiedadesService = propiedadesService;
            _vendedoresService = vendedoresService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var model = new PropiedadCreacionVM();
            model.Vendedores = await ObtenerVendedores();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PropiedadCreacionVM model)
        {
            if (model.ImagenPropiedad is null)
            {
                ModelState.AddModelError(nameof(model.ImagenPropiedad), "La imagen es requerida.");
            }

            if (ModelState.IsValid)
            {
                var response = await _propiedadesService.Crear(model, _env.WebRootPath);

                if (!response.Success)
                {
                    ViewData["Error"] = response.Message;
                    return View(model);
                }

                TempData["Success"] = response.Message;
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                model.Vendedores = await ObtenerVendedores();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Admin", "Home");
            }

            var propiedad = await _propiedadesService.ObtenerPorId(id);

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
        public async Task<IActionResult> Editar(PropiedadCreacionVM model)
        {
            if (ModelState.IsValid)
            {
                var vendedor = await _vendedoresService.ObtenerPorId(model.Propiedad.VendedorId);

                if(vendedor is null)
                {
                    TempData["Error"] = "El Vendedor no existe";
                    return RedirectToAction("Admin", "Home");
                }

                var response = await _propiedadesService.Editar(model, _env.WebRootPath);

                if (!response.Success)
                {
                    ViewData["Error"] = response.Message;
                    return View(model);
                }

                TempData["Success"] = "La propiedad se correctamente";
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                model.Vendedores = await ObtenerVendedores();
                return View(model);
            }
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var propiedad = await _propiedadesService.ObtenerPorId(id);

            if (propiedad is null)
            {
                TempData["Error"] = $"La propiedad con el ID {propiedad.Id} no fue encontrada.";
                return RedirectToAction("Admin", "Home");
            }

            var response = await _propiedadesService.Eliminar(propiedad, _env.WebRootPath);

            if (response.Success)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                TempData["Error"] = response.Message;
                return RedirectToAction("Admin", "Home");
            }
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerVendedores()
        {
            var vendedores = await _vendedoresService.ObtenerLista();
            return vendedores.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = $"{x.Nombre} {x.Apellido}"
            });
        }
    }
}
