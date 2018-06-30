using Order_Food.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order_Food.Controllers
{
    public class UsingUController : Controller
    {
        // GET: UsingU
        //อิอิ นี่คือสารท้ารบจากกลุ่มคนผู้เร่าร้อนจนมอดไหม้
        //หากเจ้าแน่จิงจงตามข้ามา 
        //หึหึหึหึหึหึหึ
        public ActionResult Admin()
        {
            ViewBag.test = "เข้าหน้า แอดมิน";
            string checklog;
            try
            {
                checklog = TempData["testsend"].ToString();
                ViewBag.checktestfromecon = checklog;
            }
            catch
            {
                ViewBag.checktestfromecon = null;
            }

            return View();
        }
        public ActionResult Customer()
        {
            ViewBag.test = "เข้าหน้า customoer";
            return View();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult update_customer(Add_Store obj, HttpPostedFileBase getimg)
        {
            if (obj.description != null)
            {
                if (getimg != null)
                {
                    //----------------------new_folder------------------------
                    string checkid = Session["namesec"].ToString();
                    string name_folder = Path.Combine(Server.MapPath("~/img/user_img"), checkid);
                    //----------------------name and save img------------------------
                    string fileName = Path.GetFileNameWithoutExtension(getimg.FileName);
                    string extention = Path.GetExtension(getimg.FileName);
                    fileName = fileName + DateTime.Now.ToString("yyyyMMdd") + extention;
                    string path = Path.Combine(Server.MapPath("~/img/user_img" + "/" + checkid), fileName);
                    if (System.IO.Directory.Exists(name_folder))
                    {
                        using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
                        {
                            getimg.SaveAs(path);
                            obj.pic = fileName;
                            obj.name = checkid;
                            db.Add_Store.Add(obj);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
                        {
                            Directory.CreateDirectory(name_folder);
                            getimg.SaveAs(path);
                            obj.pic = fileName;
                            obj.name = checkid;
                            db.Add_Store.Add(obj);
                            db.SaveChanges();
                        }

                    }
                    //List<string> myList = new List<string>();
                    //Session["var"] = myList;
                }
            }
            else
            {
                using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
                {
                    string checkid = Session["namesec"].ToString();
                    var img = db.Add_Store.Where(s => s.name == checkid).Select(s => s.pic).ToList();
                    return View(img);
                }
            }
            //string fileName = Path.GetFileName(Food_Picture_pic.FileName);
            //string path = Path.Combine(Server.MapPath("~/img/user_img"), fileName);
            //Food_Picture_pic.SaveAs(path);
            return RedirectToAction("Homepage", "Index");
        }
        //[AcceptVerbs(HttpVerbs.Post)]
        public List<Add_Store> loop_img()
        {
            string checkid = Session["namesec"].ToString();
            //List<string> img;
            List<Add_Store> model = new List<Add_Store>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                model = db.Add_Store.Where(s => s.name == checkid).ToList();

            }
            return model;
        }
        public ActionResult Show_food()
        {
            List<Food_Picture> show_food = new List<Food_Picture>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                show_food = db.Food_Picture.ToList();
                ViewBag.check = show_food.Count;
            }
            var a = show_food;
            return View(a);
        }
        public ActionResult showsaveimg()
        {
            ViewBag.folder = Session["namesec"].ToString();
            var s = loop_img();
            return View(s);
        }
        public ActionResult presaveshow()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Showcus_to_use(string getasd)
        {
            List<Add_Store> show_customer = new List<Add_Store>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {

                show_customer = db.Add_Store.ToList();
            }
            var a = show_customer;
            ViewBag.test = getasd;
            return View(a);
        }
        public ActionResult Homecustomer()
        {

            return View();
        }
        public ActionResult Homeaddstore()
        {

            return View();
        }

    }
}