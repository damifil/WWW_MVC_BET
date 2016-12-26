using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class DeleteUserView
    {
       public bool deleteU { get; set; }
        public DeleteUserView()
        {
            deleteU = false;
        }
    }
}