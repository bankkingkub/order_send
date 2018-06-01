using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order_Food.Models;
namespace Order_Food.Controllers
{
    public class IndexController : Controller
    {

        public ActionResult Homepage()
        {
            if (Session["checklogin"] == null)
            {
                ViewBag.asd = "ยังไม่เข้าสู่ระบบ";
            }
            else {
                ViewBag.asd = "เข้าสู่ระบบ";
                ViewBag.asdasd = Session["namesec"];
            }
            return View();
        }
        public ActionResult Resgister(User obj)
        {
            if (obj.User_user != null) {
                using (Order_Food_dbEntities db = new Order_Food_dbEntities())
                {
                    try
                    {
                        db.Users.Add(obj);
                        db.SaveChanges();
                        ViewBag.ab = "ใส้ข้อมูลสำเร็จ";
                        return RedirectToAction("Login", "Index");
                    }
                    catch
                    {
                        ViewBag.ab = "เอะมีปัญหา";
                    }

                }
            }
            else {
                ViewBag.ab = "มีข้าว่างใส้ช้อมูลไม่สำเร็จ";
            }
            return View();
        }
        public ActionResult Login() {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string FirstName, string pw) {
            ViewBag.dd = "เข้าหน้า login";
            Boolean checklogin;
            using (Order_Food_dbEntities db = new Order_Food_dbEntities())
            {
                var loguser = db.Users.Where(s => s.User_user == FirstName && s.User_pw == pw).FirstOrDefault();
 //               var test = (from a in db.user_account
 //                           where a.name.Equals(FirstName)
 //                           select a).ToList();
                if (loguser != null)
                {
                    string nameuser = db.Users.Where(u => u.User_user == FirstName).Select(u => u.User_name).Single();
                    checklogin = true;
                    Session["checklogin"] = checklogin;
                    Session.Add("namesec", nameuser);
                    return RedirectToAction("Homepage", "Index");
                }
                else
                {  
                    ViewBag.testlogin = "เข้าสู่ระบบไม่สำเร็จ";
                }
                return View();
            }
        }
        public ActionResult Editpw() {


            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logout() {
            Session.Clear();
            return RedirectToAction("Homepage", "Index");
        }
    }
}