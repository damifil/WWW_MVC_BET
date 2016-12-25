using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class UserSettingView
    {
        public EmailView emailView { get; set; }
        public PasswordView passwordView { get; set; }
        public ImageView imageView { get; set; }

        public UserSettingView()
        {
            this.emailView = new EmailView();
            this.passwordView = new PasswordView();
            this.imageView = new ImageView();
        }
    }
}