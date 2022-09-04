using BaseCore.Entidades;
using BaseCore.Entidades.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseCore.Controllers.Admin
{
    public class CuentasController: Controller
    {
        private readonly UserManager<Usuario> _userManager;

        public CuentasController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
