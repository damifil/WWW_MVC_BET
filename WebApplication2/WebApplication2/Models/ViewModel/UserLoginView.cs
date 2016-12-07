using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class UserLoginView   //model uzywany przy logowaniu
    {
        [Key]
        [Required(ErrorMessage = "Podaj login")]
        public string Login { get; set;}

        [Required(ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}