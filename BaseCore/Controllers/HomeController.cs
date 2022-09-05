using BaseCore.Models;
using BaseCore.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaseCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropiedadesRepositorio _propiedadesRepositorio;

        public HomeController(ILogger<HomeController> logger,
            IPropiedadesRepositorio propiedadesRepositorio)
        {
            _logger = logger;
            _propiedadesRepositorio = propiedadesRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.inicio = "inicio";
            ViewBag.anuncios = await _propiedadesRepositorio.ObtenerListaAnuncios(limite: 3);
            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

        public async Task<IActionResult> Anuncios()
        {
            ViewBag.anuncios = await _propiedadesRepositorio.ObtenerListaAnuncios();
            return View();
        }

        public async Task<IActionResult> Anuncio(int? id)
        {
            if(id is null)
            {
                return RedirectToAction("Index");
            }
            var model = await _propiedadesRepositorio.ObtenerPorId(id);

            if(model is null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Entrada()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}