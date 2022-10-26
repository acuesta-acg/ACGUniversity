using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UniversityWeb.Models
{
    public class LoginViewModel
    {
        [BindProperty]
        // [Display(Name = "Correo electrónico", Prompt = "tuemail@tudominio.com")]
        [Display(Name = "Usuario", Prompt = "miUsuario")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
        //    ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        //[DataType(DataType.EmailAddress)]
        [DataType(DataType.Text)]
        // public string Email { get; set; }
        public string Usuario { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Contraseña", Prompt = "password")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(15, ErrorMessage = "Longitud entre 6 y 15 caracteres.",
                      MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Recordarme")]
        public bool Recordarme { get; set; }

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
