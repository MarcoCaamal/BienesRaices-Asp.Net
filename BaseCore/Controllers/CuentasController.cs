using Domain.Entidades.Cuentas;
using Domain.Entidades.Cuentas.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseCore.Controllers
{
    public class CuentasController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<Usuario> _logger;
        private readonly SignInManager<Usuario> _signInManager;

        public CuentasController(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ILogger<Usuario> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new UsuarioLoginVM();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Incorrecto");
                return View(model);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register()
        {
            var existeUsuario = await _userManager.FindByEmailAsync("admin@admin.com");

            if (existeUsuario == null)
            {
                var admin = new Usuario() { Email = "admin@admin.com" };

                var resultado = await _userManager.CreateAsync(admin, password: "bienesraices2022");

                if (!resultado.Succeeded)
                {
                    foreach (var error in resultado.Errors)
                    {
                        _logger.LogError(error.Description);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Seed()
        {
            var response = await _userManager.CreateAsync(new Usuario() 
            { 
                Email = "admin@admin.com"
            }, "password");
            return Ok(response);
        }
    }
}
