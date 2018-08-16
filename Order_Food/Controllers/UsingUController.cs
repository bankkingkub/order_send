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
        public ActionResult update_customer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult update_customer(Add_Store obj, HttpPostedFileBase getimg)
        {

            if (obj.description != null)
            {
                if (getimg != null)
                {
                    //----------------------new_folder------------------------
                    string checkid = Session["user_user"].ToString();
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
                            //List<string> get_list = get_check.ToList();
                            //List<string> get_name = getname.ToList();
                            //get_cat.category = get_list.ToString();
                            //get_cat.customer_name = get_name.ToString();
                            //db.Get_catagory.Add(get_cat);
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
                    string checkid = Session["user_user"].ToString();
                    var img = db.Add_Store.Where(s => s.name == checkid).Select(s => s.pic).ToList();
                    return View(img);
                }
            }
            //string fileName = Path.GetFileName(Food_Picture_pic.FileName);
            //string path = Path.Combine(Server.MapPath("~/img/user_img"), fileName);
            //Food_Picture_pic.SaveAs(path);
            return RedirectToAction("Homeaddstore", "UsingU");
        }

        //[HttpPost]
        //public ActionResult del_pic(int get_del)
        //{
        //    using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
        //    {
        //        var del_pic = db.Add_Store.Single(x => x.id == get_del);
        //        db.Add_Store.Remove(del_pic);
        //        db.SaveChanges();
        //    }
        //    return View();
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        public List<Add_Store> loop_img()
        {
            string checkid = Session["user_user"].ToString();
            //List<string> img;
            List<Add_Store> model = new List<Add_Store>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                model = db.Add_Store.Where(s => s.name == checkid).ToList();

            }
            return model;
        }

        public ActionResult showsaveimg()
        {
            ViewBag.folder = Session["user_user"].ToString();
            var s = loop_img();
            return View(s);
        }
        [HttpPost]
        public ActionResult showsaveimg(int get_del)
        {
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var del_pic = db.Add_Store.Single(x => x.id == get_del);
                db.Add_Store.Remove(del_pic);
                db.SaveChanges();
            }
            return View();
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
            if (Session["namesec"] == null)
            {
                return RedirectToAction("Homepage", "Index");
            }
            return View();
        }
        //public List<Category> loop_Category()
        //{
        //    List<Category> loop_cat = new List<Category>();
        //    using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
        //    {
        //        loop_cat = db.Category.ToList();
        //    }
        //    return loop_cat;
        //}
        public ActionResult Category()
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                List<Category> loob_cat = new List<Category>();
                loob_cat = db.Category.ToList();
                var check = db.Get_catagory.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.storechek = "false";
                    return View(loob_cat);
                }
                else
                {
                    var get_namescat = db.Get_catagory.Single(x => x.customer_name == checkid);
                    if (get_namescat.edit_value == "edit")
                    {
                        ViewBag.storechek = "false";
                        return View(loob_cat);
                    }
                    else
                    {
                        ViewBag.storechek = "true";
                        ViewBag.cat01 = db.Get_catagory.Where(x => x.customer_name == checkid).Select(x => x.category01).FirstOrDefault();
                        ViewBag.cat02 = db.Get_catagory.Where(x => x.customer_name == checkid).Select(x => x.category02).FirstOrDefault();
                        ViewBag.cat03 = db.Get_catagory.Where(x => x.customer_name == checkid).Select(x => x.category03).FirstOrDefault();
                    }
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult Category(string[] ajex_cat, Get_catagory get_catagory)
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_catagory.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check != null)
                {
                    //เลือกที่เซฟจะไป เเล้ว เซฟ
                    var get_namecat = db.Get_catagory.Single(x => x.customer_name == checkid);
                    //save สถานะ
                    get_namecat.edit_value = get_catagory.edit_value;
                    db.SaveChanges();
                    if (get_namecat.edit_value == "show")
                    {
                        //save ที่แก้ไชมาเเล้ว
                        int i = ajex_cat.Length;
                        get_catagory.customer_name = checkid;
                        switch (i)
                        {
                            case 1:
                                get_namecat.category01 = ajex_cat[0];
                                get_namecat.category02 = null;
                                get_namecat.category03 = null;
                                break;
                            case 2:
                                get_namecat.category01 = ajex_cat[0];
                                get_namecat.category02 = ajex_cat[1];
                                get_namecat.category03 = null;
                                break;
                            case 3:
                                get_namecat.category01 = ajex_cat[0];
                                get_namecat.category02 = ajex_cat[1];
                                get_namecat.category03 = ajex_cat[2];
                                break;
                        }
                        db.SaveChanges();
                    }
                }
                else
                {
                    //save ครั้งฃเเรก
                    int i = ajex_cat.Length;
                    get_catagory.customer_name = checkid;
                    switch (i)
                    {
                        case 1:
                            get_catagory.category01 = ajex_cat[0];
                            break;
                        case 2:
                            get_catagory.category01 = ajex_cat[0];
                            get_catagory.category02 = ajex_cat[1];
                            break;
                        case 3:
                            get_catagory.category01 = ajex_cat[0];
                            get_catagory.category02 = ajex_cat[1];
                            get_catagory.category02 = ajex_cat[2];
                            break;
                    }
                    db.Get_catagory.Add(get_catagory);
                    db.SaveChanges();

                }
                //List<user_account> ad_st = new List<user_account> { new user_account { name = "", last = "" } };
                //var ab = model_car();
                //List<Category> loob_cat = new List<Category>();
                //using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
                //{
                //    loob_cat = db.Category.ToList();
                //}
                //if (check != null)
                //{
                //    ViewBag.storechek = "true";
                //    ลูปจาก db เพื่อเอามาใช่งาน
                //    List<string> a = new List<string>();
                //    a = db.Get_catagory.Where(x => x.customer_name == checkid).Select(x => x.category01).ToList();
                //    ViewBag.show_cat = a;
                //    return View();
                //}
                //else
                //{
                //    if (ajex_cat != null)
                //    {
                //        int i = ajex_cat.Length;
                //        for (int ch = 0; ch < i; ch++)
                //        {
                //            get_cat.category01 = ajex_cat[ch];
                //            get_cat.customer_name = checkid;
                //            db.Get_catagory.Add(get_cat);
                //            db.SaveChanges();
                //        }
                //    }
                //    else
                //    {
                //        ViewBag.storechek = "false";
                //    }
                //}
            }
            //return View(ad_st);
            return View();
        }

        public Add_Store model_car()
        {
            Add_Store modeluser = new Add_Store();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                modeluser.category2 = db.Category.ToList<Category>();
            }
            return modeluser;
        }

        //public int test_add()
        //{
        //    int cut = Convert.ToInt32(Session["cut"]);
        //    cut = cut + 1;
        //    Session["checklogin"] = cut;
        //    return cut;
        //}

        //public ActionResult getnamestore(Get_storename get_storename)
        //{
        //    string checkid = Session["namesec"].ToString();

        //    using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
        //    {

        //        var check = db.Get_storename.Where(x => x.customer_name == checkid).FirstOrDefault();
        //        var get_namesto = db.Get_storename.Single(x => x.customer_name == checkid);
        //        if (get_storename.status != null)
        //        {
        //            get_namesto.status = get_storename.status;
        //            db.SaveChanges();
        //        }
        //        if (get_namesto.status == "edit")
        //        {
        //            ViewBag.storechek = "false";
        //        }
        //        else
        //        {

        //            if (check != null)
        //            {
        //                ViewBag.storechek = "true";
        //                ViewBag.namestore = db.Get_storename.Where(x => x.customer_name == checkid).Select(x => x.get_storename1).FirstOrDefault();
        //            }
        //            else
        //            {
        //                if (get_storename.get_storename1 != null && get_namesto.status == "show")
        //                {
        //                    get_storename.customer_name = checkid;
        //                    db.Get_storename.Add(get_storename);
        //                    db.SaveChanges();
        //                    ViewBag.storechek = "true";
        //                    ViewBag.namestore = db.Get_storename.Where(x => x.customer_name == checkid).Select(x => x.get_storename1).FirstOrDefault();
        //                }
        //                else
        //                {
        //                    ViewBag.storechek = "false";
        //                }
        //            }

        //        }


        //    }
        //    return View();
        //}
        public ActionResult getnamestore()
        {

            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_storename.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.storechek = "false";
                }
                else
                {
                    var get_namesto = db.Get_storename.Single(x => x.customer_name == checkid);
                    if (get_namesto.edit_value == "edit")
                    {
                        ViewBag.storechek = "false";
                        ViewBag.namestore = db.Get_storename.Where(x => x.customer_name == checkid).Select(x => x.get_storename1).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.storechek = "true";
                        ViewBag.namestore = db.Get_storename.Where(x => x.customer_name == checkid).Select(x => x.get_storename1).FirstOrDefault();
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult getnamestore(Get_storename get_storename)
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_storename.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check != null)
                {
                    var get_namesto = db.Get_storename.Single(x => x.customer_name == checkid);
                    get_namesto.edit_value = get_storename.edit_value;
                    db.SaveChanges();
                    if (get_namesto.edit_value == "show")
                    {
                        get_namesto.get_storename1 = get_storename.get_storename1;
                        db.SaveChanges();
                    }
                }
                else
                {
                    get_storename.customer_name = checkid;
                    db.Get_storename.Add(get_storename);
                    db.SaveChanges();

                }

            }
            return View();
        }
        //[HttpPost]
        //public ActionResult getnamestore(string name) {

        //    ViewBag.get = "เข้า สู่ get นะไอน้อง";


        //    return View();
        //}
        //public string get_ajex(string name) {

        //    var getstring = name;

        //    return getstring;
        //}
        public ActionResult Get_c_address()
        {
            var checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_location.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.storechek = "false";
                }
                else
                {
                    var get_namesto = db.Get_location.Single(x => x.customer_name == checkid);
                    if (get_namesto.edit_value == "edit")
                    {
                        ViewBag.storechek = "false";
                        ViewBag.street = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_street).FirstOrDefault();
                        ViewBag.district = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_district).FirstOrDefault();
                        ViewBag.county = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_county).FirstOrDefault();
                        ViewBag.province = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_province).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.storechek = "true";
                        ViewBag.street = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_street).FirstOrDefault();
                        ViewBag.district = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_district).FirstOrDefault();
                        ViewBag.county = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_county).FirstOrDefault();
                        ViewBag.province = db.Get_location.Where(x => x.customer_name == checkid).Select(x => x.address_province).FirstOrDefault();
                    }
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Get_c_address(Get_location get_lo)
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_location.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check != null)
                {
                    var get_location_ed = db.Get_location.Single(x => x.customer_name == checkid);
                    get_location_ed.edit_value = get_lo.edit_value;
                    db.SaveChanges();
                    if (get_location_ed.edit_value == "show")
                    {
                        get_location_ed.address_street = get_lo.address_street;
                        get_location_ed.address_district = get_lo.address_district;
                        get_location_ed.address_county = get_lo.address_county;
                        get_location_ed.address_province = get_lo.address_province;
                        db.SaveChanges();
                    }
                }
                else
                {
                    get_lo.customer_name = checkid;
                    db.Get_location.Add(get_lo);
                    db.SaveChanges();
                }
            }
            return View();


        }

        public ActionResult Get_time_store()
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_time.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.storechek = "false";
                }
                else
                {
                    var get_namesto = db.Get_time.Single(x => x.customer_name == checkid);
                    if (get_namesto.edit_value == "edit")
                    {
                        ViewBag.storechek = "false";
                        ViewBag.dayopen = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.day_open).FirstOrDefault();
                        ViewBag.dayclose = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.day_close).FirstOrDefault();
                        ViewBag.timeopen = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.time_open).FirstOrDefault();
                        ViewBag.timeclose = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.time_close).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.storechek = "true";
                        ViewBag.dayopen = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.day_open).FirstOrDefault();
                        ViewBag.dayclose = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.day_close).FirstOrDefault();
                        ViewBag.timeopen = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.time_open).FirstOrDefault();
                        ViewBag.timeclose = db.Get_time.Where(x => x.customer_name == checkid).Select(x => x.time_close).FirstOrDefault();
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Get_time_store(Get_time get_time)
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_time.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check != null)
                {
                    var get_nametim = db.Get_time.Single(x => x.customer_name == checkid);
                    get_nametim.edit_value = get_time.edit_value;
                    db.SaveChanges();
                    if (get_nametim.edit_value == "show")
                    {
                        get_nametim.day_open = get_time.day_open;
                        get_nametim.day_close = get_time.day_close;
                        get_nametim.time_open = get_time.time_open;
                        get_nametim.time_close = get_time.time_close;
                        db.SaveChanges();
                    }
                }
                else
                {
                    get_time.customer_name = checkid;
                    db.Get_time.Add(get_time);
                    db.SaveChanges();
                }

            }

            return View();
        }
        public ActionResult Add_food()
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_menu.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.storechek = "false";
                }
                else
                {
                    var get_namesme = db.Get_menu.Where(x => x.customer_name == checkid).Select(x => x.edit_value).FirstOrDefault();
                    if (get_namesme == "edit")
                    {
                        ViewBag.storechek = "edit";
                        List<Get_menu> show_name = new List<Get_menu>();
                        show_name = db.Get_menu.Where(x => x.customer_name == checkid).ToList();
                        return View(show_name);
                    }
                    else
                    {
                        ViewBag.storechek = "true";
                        List<Get_menu> show_name = new List<Get_menu>();
                        show_name = db.Get_menu.Where(x => x.customer_name == checkid).ToList();
                        return View(show_name);
                    }
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Add_food(string[] get_name_menu, string[] get_name_price, Get_menu get_menu)
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Get_menu.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check != null)
                {
                    //List<Get_menu> get_namemenu = new List<Get_menu>();
                    var get_namemenu = db.Get_menu.Where(x => x.customer_name == checkid).First();

                    var a = db.Get_menu.Where(x => x.customer_name == checkid).Select(x => x.edit_value);
                    foreach (var obj in db.Get_menu.Where(x => x.customer_name == checkid))
                    {
                        obj.edit_value = get_menu.edit_value;
                    }
                    db.SaveChanges();

                    if (get_namemenu.edit_value == "show")
                    {
                        var data = from customer_name in db.Get_menu where customer_name.customer_name == checkid select customer_name;
                        foreach (var remove in data)
                        {
                            db.Get_menu.Remove(remove);
                        }
                        db.SaveChanges();

                        int i = get_name_menu.Length;
                        for (int ch = 0; ch < i; ch++)
                        {
                            get_menu.customer_name = checkid;
                            get_menu.Menu_name = get_name_menu[ch];
                            get_menu.Menu_price = get_name_price[ch];
                            db.Get_menu.Add(get_menu);
                            db.SaveChanges();
                        }

                        //int i = get_name_menu.Length;
                        //for (int ch = 0; ch < i; ch++)
                        //{
                        //    get_namemenu.Menu_name = get_name_menu[ch];
                        //    get_namemenu.Menu_price = get_name_price[ch];
                        //    db.SaveChanges();
                        //}
                    }
                }
                else
                {
                    int i = get_name_menu.Length;
                    for (int ch = 0; ch < i; ch++)
                    {
                        get_menu.customer_name = checkid;
                        get_menu.Menu_name = get_name_menu[ch];
                        get_menu.Menu_price = get_name_price[ch];
                        db.Get_menu.Add(get_menu);
                        db.SaveChanges();
                    }
                }

            }

            return View();
        }
        public ActionResult Add_store_img()
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var img = db.Add_store_img.Where(x => x.customer_name == checkid).Select(x => x.add_img).ToList();
                ViewBag.folder = checkid + "/store_pic";
                return View(img);
            }
        }
        [HttpPost]
        public ActionResult Add_store_img(HttpPostedFileBase getimg_store, Add_store_img obj)
        {
            //----------------------new_folder------------------------
            string checkid = Session["user_user"].ToString();
            string name_folder = Path.Combine(Server.MapPath("~/img/user_img"), checkid, "store_pic");
            //----------------------name and save img------------------------
            string fileName = Path.GetFileNameWithoutExtension(getimg_store.FileName);
            string extention = Path.GetExtension(getimg_store.FileName);
            fileName = fileName + DateTime.Now.ToString("yyyyMMdd") + extention;
            string path = Path.Combine(Server.MapPath("~/img/user_img" + "/" + checkid), "store_pic", fileName);
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                if (System.IO.Directory.Exists(name_folder))
                {
                    getimg_store.SaveAs(path);
                    var get_img = db.Add_store_img.Single(x => x.customer_name == checkid);
                    get_img.add_img = fileName;
                    db.SaveChanges();
                }
                else
                {
                    Directory.CreateDirectory(name_folder);
                    getimg_store.SaveAs(path);
                    obj.add_img = fileName;
                    obj.customer_name = checkid;
                    db.Add_store_img.Add(obj);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Homeaddstore", "UsingU");
        }
        public ActionResult Add_description()
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Description.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.storechek = "false";
                }
                else
                {
                    var get_namesto = db.Description.Single(x => x.customer_name == checkid);
                    if (get_namesto.edit_value == "edit")
                    {
                        ViewBag.storechek = "false";
                        ViewBag.name = db.Description.Where(x => x.customer_name == checkid).Select(x => x.description1).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.name = db.Description.Where(x => x.customer_name == checkid).Select(x => x.description1).FirstOrDefault();
                        ViewBag.storechek = "true";
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Add_description(Description get_description)
        {
            string checkid = Session["user_user"].ToString();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                var check = db.Description.Where(x => x.customer_name == checkid).FirstOrDefault();
                if (check != null)
                {
                    var get_descrpt = db.Description.Single(x => x.customer_name == checkid);
                    get_descrpt.edit_value = get_description.edit_value;
                    db.SaveChanges();
                    if (get_descrpt.edit_value == "show")
                    {
                        get_descrpt.description1 = get_description.description1;
                        db.SaveChanges();
                    }
                }
                else
                {
                    get_description.customer_name = checkid;
                    db.Description.Add(get_description);
                    db.SaveChanges();
                }
            }

            return View();
        }


        public ActionResult Show_food()
        {
            List<View_showing> show_food = new List<View_showing>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                show_food = db.View_showing.ToList();
                ViewBag.check = show_food.Count;
            }
            var a = show_food;
            return View(a);
        }
        [HttpPost]
        public ActionResult Show_food(string get_name)
        {
            Session["get_name_sto"] = get_name;
            return RedirectToAction("Home_show_food_detile", "UsingU");
        }
            public ActionResult More_show_food()
        {
            List<View_showing> show_food = new List<View_showing>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                show_food = db.View_showing.ToList();
                ViewBag.check = show_food.Count;
            }
            var a = show_food;
            return View(a);
        }
        public ActionResult Home_show_food_detile()
        {
            var get_sec = Session["get_name_sto"].ToString();
            ViewBag.get_name = get_sec;
            return View();
        }
        [HttpPost]
        public ActionResult Home_show_food_detile(string get_name)
        {
            return View();
        }
    }
}