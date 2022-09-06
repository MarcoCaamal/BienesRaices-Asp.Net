using Domain.Entidades.Propiedades;
using Domain.Entidades.Vendedores;
using Microsoft.AspNetCore.Mvc;
using Services.IServices.Vendedores;

namespace BaseCore.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly IVendedoresService _vendedoresService;

        public VendedoresController(IVendedoresService vendedoresService)
        {
            _vendedoresService = vendedoresService;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Vendedor model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vendedoresService.Crear(model);

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

            var vendedor = await _vendedoresService.ObtenerPorId(id);

            if (vendedor is null)
            {
                TempData["Error"] = "La propiedad no existe.";
                return RedirectToAction("Admin", "Home");
            }

            return View(vendedor);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Vendedor model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vendedoresService.Editar(model);

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
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var vendedor = await _vendedoresService.ObtenerPorId(id);

            if(vendedor is null)
            {
                TempData["Error"] = $"El vendedor con ID {id} no fue encontrado(a)";
                return RedirectToAction("Admin", "Home");
            }

            var response = await _vendedoresService.Eliminar(id);

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
    }
}
