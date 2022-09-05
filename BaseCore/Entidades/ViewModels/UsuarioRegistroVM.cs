﻿using System.ComponentModel.DataAnnotations;

namespace BaseCore.Entidades.ViewModels
{
    public class UsuarioRegistroVM
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} no contiene un Email valido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Password { get; set; }
    }
}
