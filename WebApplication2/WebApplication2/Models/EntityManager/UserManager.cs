using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Models.EntityManager
{
    public class UserManager
    {
        public void AddUserAccount(UserSignUpView newUser)  // dodanie użytkownika 
        {
            using (ProjektEntities db = new ProjektEntities())
            {
                USER user = new USER();
                user.User_ID = newUser.Login;
                user.Password = newUser.Password;
                user.e_mail = newUser.Email;
                user.Total_score = "0";
                user.Is_Admin = false;

                db.USER.Add(user);
                db.SaveChanges();

            }
        }
        public bool IsLoginExist(string loginName)    // sprawdzenie czy login istnieje w bazie danych
        {
            using (ProjektEntities db = new ProjektEntities())
            {
                return db.USER.Where(u => u.User_ID.Equals(loginName)).Any();
            }
        }

        public string GetUserPassword(string login)     //uzyskanie hasła danego użytkownika z bazy danych
        {
            using (ProjektEntities db = new ProjektEntities())
            {
                var user = db.USER.Where(u => u.User_ID.ToLower().Equals(login));
                if (user.Any())
                {
                    return user.FirstOrDefault().Password;
                }
                else
                {
                    return string.Empty;
                }
            }
        }


    }
   
}