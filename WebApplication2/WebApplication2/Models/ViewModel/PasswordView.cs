using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class PasswordView
    {
        [Required(ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Podaj nowe hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Podaj stare hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Podaj stare hasło:")]
        public string oldPassword { get; set; }
    }
}