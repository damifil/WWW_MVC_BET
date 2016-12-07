using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class LoginRegisterView
    {
        public UserLoginView UserLoginView { get; set; }
        public UserSignUpView UserSignUpView { get; set; }
    }
}