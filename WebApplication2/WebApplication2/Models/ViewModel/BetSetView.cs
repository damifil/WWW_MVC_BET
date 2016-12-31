using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class BetSetView
    {

        public int BetID { get; set; }
        [Required(ErrorMessage = "Wybierz zawodnika")]
        public string Driver_Name1 { get; set; }
        [Required(ErrorMessage = "Wybierz zawodnika")]
        public string Driver_Name2 { get; set; }
        [Required(ErrorMessage = "Wybierz zawodnika")]
        public string Driver_Name3 { get; set; }
        [Required(ErrorMessage = "Wybierz zawodnika")]
        public string Driver_Time1 { get; set; }

        public BetSetView()
        {

        }

    }
}