using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class ImageView
    {
        public byte[] imageData { get; set; }

        [Required(ErrorMessage = "Proszę wybrać zdjęcie.")]
        public HttpPostedFileBase  File { get; set; }

        public ImageView()
        {
            this.imageData = null;
            this.File = null;
        }
    }
}