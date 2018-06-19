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
        public ActionResult Resgister(User obj, Food_Picture img, HttpPostedFileBase Food_Picture_pic, string Direction)
        {
            if (obj.User_user != null)
            {
                using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
                {
                    if (!ModelState.IsValid)
                    {
                        return View();
                    }
                    obj.Status = Direction;
                    db.User.Add(obj);
                    db.SaveChanges();
                    ViewBag.ab = "ใส้ข้อมูลสำเร็จ";
                    if (Food_Picture_pic != null && Food_Picture_pic.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(Food_Picture_pic.FileName);
                        string path = Path.Combine(Server.MapPath("~/img/user_img"), fileName);
                        Food_Picture_pic.SaveAs(path);
                        img.Food_Picture_pic = fileName;
                        img.Food_Picture_name = obj.User_user;
                        db.Food_Picture.Add(img);
                        db.SaveChanges();
                        Food_Picture newrow = new Food_Picture();
                        ViewData.Model = db.Food_Picture;
                        ViewBag.savestage = "บันทึกเรียบร้อย";
                    }
                    else
                    {
                        img.Food_Picture_name = obj.User_user;
                        img.Food_Picture_pic = "noimg.jpg";
                        db.Food_Picture.Add(img);
                        db.SaveChanges();
                    }
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
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var loguser = db.User.Where(s => s.User_user == FirstName && s.User_pw == pw).FirstOrDefault();
                //               var test = (from a in db.user_account
                //                           where a.name.Equals(FirstName)
                //                           select a).ToList();
                if (loguser != null)
                {
                    string nameuser = db.User.Where(u => u.User_user == FirstName).Select(u => u.User_name).Single();
                    checklogin = true;
                    Session["checklogin"] = checklogin;
                    string checkstatus = db.User.Where(u => u.User_user == FirstName).Select(u => u.Status).Single();
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
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                try
                {
                    //ModelState.Remove("User_repw");
                    var userModel = db.User.Single(u => u.User_user == user);
                    userModel.User_pw = re_pw;
                    db.SaveChanges();
                    ViewBag.regis = "ใส่ข้อมูลเรียบร้อย";
                }
                catch
                {
                    ViewBag.regis = "ไม่สามรถแก้ไขได้";
                }

                return View();
            }
        }
        public ActionResult UploadFile()
        {
            //if (Food_Picture_pic != null && Food_Picture_pic.ContentLength > 0)
            //{
            //    using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            //    {
            //        string fileName = Path.GetFileName(Food_Picture_pic.FileName);
            //        string path = Path.Combine(Server.MapPath("~/img/user_img"), fileName);

            //        string fileName = System.IO.Path.GetFileName(Food_Picture_pic.FileName);
            //        string path = Server.MapPath("~/img/user_img" + fileName);
            //        Food_Picture_pic.SaveAs(path);
            //        obj.Food_Picture_pic = fileName;
            //        db.Food_Picture.Add(obj);
            //        db.SaveChanges();
            //        ViewBag.savestage = "บันทึกเรียบร้อย";
            //    }
            //}
            //else
            //{
            //    ViewBag.savestage = "เอะมีปัญหา";
            //}
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                try
                {
                    string user_check = Session["namesec"].ToString();
                    var get_img = db.Food_Picture.Where(c => c.Food_Picture_name == user_check).Select(c => c.Food_Picture_pic).Single();
                    ViewBag.get_img = get_img;
                }
                catch
                {

                    return RedirectToAction("Homepage", "Index");
                }

            }
            return View();
        }
    }
}