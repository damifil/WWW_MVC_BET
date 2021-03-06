﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class UserSignUpView   // model używany przy rejestracji
    {
        [Key]
        [Required(ErrorMessage = "Podaj login")]
        [Display(Name = "Login:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Podaj e-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

    }

}