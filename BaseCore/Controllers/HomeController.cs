using BaseCore.Models;
using Domain.Entidades.Contacto;
using Domain.Entidades.Cuentas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.IServices.Contacto;
using Services.IServices.Propiedades;
using Services.IServices.Vendedores;
using System.Diagnostics;
using static Common.Enums.Enums;

namespace BaseCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropiedadesService _propiedadesService;
        private readonly IVendedoresService _vendedoresService;
        private readonly IEmailEnvioService _emailEnvioService;

        public HomeController(ILogger<HomeController> logger,
            IPropiedadesService propiedadesService,
            IVendedoresService vendedoresService,
            IEmailEnvioService emailEnvioService)
        {
            _logger = logger;
            _propiedadesService = propiedadesService;
            _vendedoresService = vendedoresService;
            _emailEnvioService = emailEnvioService;
        }


        [Route("Admin")]
        public async Task<IActionResult> Admin()
        {
            var model = new IndexAdminVM();
            model.Propiedades = await _propiedadesService.ObtenerLista();
            model.Vendedores = await _vendedoresService.ObtenerLista();
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewBag.inicio = "inicio";
            ViewBag.anuncios = await _propiedadesService.ObtenerListaParaAnuncios(numeroRegistros: 3);
            return View();
        }

        [AllowAnonymous]
        public IActionResult Nosotros()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Anuncios()
        {
            ViewBag.anuncios = await _propiedadesService.ObtenerListaParaAnuncios();
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Anuncio(int? id)
        {
            if(id is null)
            {
                return RedirectToAction("Index");
            }
            var model = await _propiedadesService.ObtenerPorId(id);

            if(model is null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Blog()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Entrada()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contacto()
        {
            ViewBag.opcionesContacto = ObtenerOpcionesContacto();
            var model = new ContactoVM();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Contacto(ContactoVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.opcionesContacto = ObtenerOpcionesContacto(model.OpcionContacto);
                return View(model);
            }
            else
            {
                var response = _emailEnvioService.enviarEmail(model);

                if (!response.Success)
                {
                    ViewData["Error"] = response.Message;
                    return View(model);
                }

                TempData["Success"] = response.Message;
                return RedirectToAction("Contacto");
            }
        }

        [AllowAnonymous]
        public IActionResult ErrorFound(int? errorCode)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<SelectListItem> ObtenerOpcionesContacto(string? opcionSeleccionada = null)
        {
            if (string.IsNullOrEmpty(opcionSeleccionada))
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Télefono", Value = "telefono"},
                    new SelectListItem() { Text = "Email", Value = "email"}
                };
            }
            else
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem() 
                    { 
                        Text = "Télefono", 
                        Value = "telefono", 
                        Selected = (opcionSeleccionada == "telefono" ? true : false) 
                    },
                    new SelectListItem() { 
                        Text = "Email", 
                        Value = "email", 
                        Selected = (opcionSeleccionada == "email" ? true : false)
                    }
                };
            }
        }
    }
}