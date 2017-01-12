using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;
namespace WebApplication2.Models.EntityManager
{
    public class BetManager
    {
        public void SetBet(string login, int race, string pos1, string pos2, string pos3, string time1)
        {
            using (ProjektEntities db = new ProjektEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        BETS itm = new BETS();
                        itm.User_ID = login;
                        itm.Date = DateTime.Now.ToString();
                        itm.Race_ID = race;
                        itm.Pos_1 = pos1;
                        itm.Pos_2 = pos2;
                        itm.Pos_3 = pos3;
                        itm.Time_1 = time1;

                        db.BETS.Add(itm);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        public bool IsBetExists(string login, int raceID)
        {
            using (ProjektEntities db = new ProjektEntities())
            {
                var be = from b in db.BETS
                         where b.User_ID == login && b.Race_ID == raceID
                         select new { b.Race_ID};

                foreach (var x in be)
                {
                    if (x.Race_ID != 0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
        }
    }
}