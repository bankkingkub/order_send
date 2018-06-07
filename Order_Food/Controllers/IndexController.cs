using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
                ViewBag.Checlogin = "ยังไม่เข้าสู่ระบบ";
            }
            else if (Session["checkstatus"].Equals("admin"))
            {
                ViewBag.Checlogin = "ยินดีต้อนรับ admin เข้าสู่ระบบ";
                ViewBag.usename = Session["namesec"];
                ViewBag.Checkpage = Session["checkstatus"];

                TempData["testsend"] = "true";
                RedirectToAction("Admin");

                return RedirectToAction("Admin", "UsingU");
            }
            else if (Session["checkstatus"].Equals("user"))
            {
                ViewBag.Checlogin = "ยินดีต้อนรับ user เข้าสู่ระบบ";
                ViewBag.usename = Session["namesec"];
                ViewBag.Checkpage = Session["checkstatus"];
            }
            else if (Session["checkstatus"].Equals("customer"))
            {
                ViewBag.Checlogin = "ยินดีต้อนรับ customer เข้าสู่ระบบ";
                ViewBag.usename = Session["namesec"];
                ViewBag.Checkpage = Session["checkstatus"];
            }
            return View();
        }
        public ActionResult Resgister(User obj)
        {
            if (obj.User_user != null)
            {
                using (Order_Food_dbEntities db = new Order_Food_dbEntities())
                {
                    if (!ModelState.IsValid)
                    {
                        return View();
                    }
                    db.Users.Add(obj);
                    db.SaveChanges();
                    ViewBag.ab = "ใส้ข้อมูลสำเร็จ";
                    return RedirectToAction("Login", "Index");



                }
            }
            else
            {
                ViewBag.ab = "มีข้าว่างใส้ช้อมูลไม่สำเร็จ";
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string FirstName, string pw)
        {
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
                    string checkstatus = db.Users.Where(u => u.User_user == FirstName).Select(u => u.Status).Single();
                    Session.Add("checkstatus", checkstatus);
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
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Homepage", "Index");
        }
        public ActionResult redit(string user, string re_pw)
        {
            ViewBag.regis = "เข้า Editpw";
            using (Order_Food_dbEntities db = new Order_Food_dbEntities())
            {
                if (user != null)
                {
                    //ModelState.Remove("User_repw");


                    var userModel = db.Users.Single(u => u.User_user == user);
                    userModel.User_pw = re_pw;
                    db.SaveChanges();
                    ViewBag.regis = "ใส่ข้อมูลเรียบร้อย";
                }
                else
                    ViewBag.regis = "ไม่สามรถแก้ไขได้";
                return View();
            }
        }
        public ActionResult userpage()
        {

            return View();
        }
        public ActionResult adminpage()
        {

            return View();
        }
        public ActionResult customer()
        {

            return View();
        }

        public ActionResult UploadFile(HttpPostedFileBase file, Food_Picture obj)
        {

            if (file != null && file.ContentLength > 0)
            {
                using (Order_Food_dbEntities db = new Order_Food_dbEntities())
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/user_img"), fileName);
                    file.SaveAs(path);
                    ViewBag.savestage = "บันทึกเรียบร้อย";
                }
            }
            else
            {
                ViewBag.savestage = "เอะมีปัญหา";
            }
            return RedirectToAction("Resgister", "Index");
        }
    }
}