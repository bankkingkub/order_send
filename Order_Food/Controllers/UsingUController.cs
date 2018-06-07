using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order_Food.Controllers
{
    public class UsingUController : Controller
    {
        // GET: UsingU
        public ActionResult Admin()
        {
            ViewBag.test = "เข้าหน้า แอดมิน";
            string checklog ;
            try {
                checklog = TempData["testsend"].ToString();
                ViewBag.checktestfromecon = checklog;
            } catch {
                ViewBag.checktestfromecon = null;
            }

            return View();
        }
        public ActionResult User()
        {
            ViewBag.test = "เข้าหน้า user";
            return View();
        }
        public ActionResult Customer()
        {
            ViewBag.test = "เข้าหน้า customoer";
            return View();
        }
    }
}