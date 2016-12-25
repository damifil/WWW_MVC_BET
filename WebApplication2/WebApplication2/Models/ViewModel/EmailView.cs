using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class EmailView
    {
        [Display(Name = "Aktualny e-mail:")]
        public string email { get; set; }

        [Required(ErrorMessage = "Podaj nowy e-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Nowy e-mail:")]
        public string newEmail { get; set; }

        [Required(ErrorMessage = "Podaj nowy e-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Potwierdź e-mail:")]
        public string confirmNewEmail { get; set; }
    }
}