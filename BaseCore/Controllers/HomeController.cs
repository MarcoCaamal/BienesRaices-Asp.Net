using BaseCore.Models;
using Domain.Entidades.Cuentas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices.Propiedades;
using Services.IServices.Vendedores;
using System.Diagnostics;

namespace BaseCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropiedadesService _propiedadesService;
        private readonly IVendedoresService _vendedoresService;

        public HomeController(ILogger<HomeController> logger,
            IPropiedadesService propiedadesService,
            IVendedoresService vendedoresService)
        {
            _logger = logger;
            _propiedadesService = propiedadesService;
            _vendedoresService = vendedoresService;
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
            return View();
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
    }
}