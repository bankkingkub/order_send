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
            List<View_how_show> home_show_food = new List<View_how_show>();
            var get_sec = get_section_store();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                home_show_food = db.View_how_show.Where(x => x.User_user == get_sec).ToList();
            }
            var re = home_show_food;
            return View(re);
        }
        public ActionResult Coment_section()
        {
            var checkid = get_section_user();
            ViewBag.get_name = checkid;
            return View();
        }
        [HttpPost]
        public ActionResult Coment_section(Coment_section send_coment)
        {
            var get_sec = get_section_store();
            var checkid = get_section_user();
            List<Coment_section> show_coment = new List<Coment_section>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                if (send_coment.edit_value == "show")
                {
                    show_coment = db.Coment_section.Where(x => x.customer_name == get_sec).ToList();
                    send_coment.user_name = checkid;
                    send_coment.customer_name = get_sec;
                    send_coment.date = DateTime.Now;
                    db.Coment_section.Add(send_coment);
                    db.SaveChanges();
                }
                else if (send_coment.edit_value == "edit")
                {
                    var edit_coment = db.Coment_section.Single(x => x.id == send_coment.id);
                    edit_coment.edit_value = send_coment.edit_value;
                    db.SaveChanges();
                }
                else if (send_coment.edit_value == "get_edit")
                {
                    var edit_coment = db.Coment_section.Single(x => x.id == send_coment.id);
                    edit_coment.coment = send_coment.coment;
                    edit_coment.edit_value = "show";
                    db.SaveChanges();
                }

            }
            return View();
        }
        public ActionResult Show_conment_section()
        {
            var get_sec = get_section_store();
            List<Coment_section> show_coment = new List<Coment_section>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                show_coment = db.Coment_section.Where(x => x.customer_name == get_sec).ToList();
            }
            ViewBag.get_name = get_section_user();
            return View(show_coment);
        }

        public ActionResult Img_section()
        {
            var get_sec = get_section_store();
            List<Add_Store> img_sec = new List<Add_Store>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                img_sec = db.Add_Store.Where(x => x.name == get_sec).ToList();
            }
            return View(img_sec);
        }
        public ActionResult toHome()
        {
            return RedirectToAction("Homepage", "Index");
        }
        public ActionResult Home_chat()
        {
            string get_anoter = get_section_store();
            List<Get_menu> get_menu = new List<Get_menu>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                get_menu = db.Get_menu.Where(x => x.customer_name == get_anoter).ToList();
            }
            return View(get_menu);
        }
        [HttpPost]
        public ActionResult Home_chat(Getvalue_order get_order, string[] senda)
        {
            string get_anoter = get_section_store();
            List<Get_menu> get_menu = new List<Get_menu>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                if (get_order.get_order != null)
                {

                    //int order_price_val = 0;
                    //for (int i = 0; i < get_order.get_order.Length; i++)
                    //{
                    //    int order_price = get_order.get_order[i]*get_order.get_price[i];
                    //    order_price_val = order_price + order_price_val;
                    //}
                    //ViewBag.price = order_price_val;
                    //ViewBag.priceasdasd = "123456";
                }
                get_menu = db.Get_menu.Where(x => x.customer_name == get_anoter).ToList();
            }
            return View(get_menu);
        }
        public ActionResult Show_chat()
        {
            string get_name = get_section_user();
            string get_anoter = get_section_store();
            List<Chat> get_chat = new List<Chat>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                try
                {
                    string get_chat_name = Session["get_chat_name"].ToString();
                    string get_check = Session["get_check"].ToString();

                    var get_non = db.Alert_customer.Single(x => x.name == get_chat_name && x.user_name == get_name);
                    if (get_non.alert == "show")
                    {
                        get_non.alert = "non";
                        get_non.date = DateTime.Today;
                        get_non.time = DateTime.Now.TimeOfDay;
                        db.SaveChanges();
                    }
                    get_chat = db.Chat.Where(x => x.customer_name == get_chat_name && x.user_name == get_name).ToList();
                    ViewBag.check = "c";
                }
                catch
                {
                    ViewBag.check = "u";
                    get_chat = db.Chat.Where(x => x.customer_name == get_name && x.user_name == get_anoter).ToList();
                }

            }
            return View(get_chat);
        }
        public ActionResult Chat_section()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Chat_section(Chat send_chat, Alert_customer alet_cu)
        {
            var get_name = get_section_user();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                try
                {
                    string get_chat_name = Session["get_chat_name"].ToString();
                    string get_check = Session["get_check"].ToString();
                    send_chat.customer_name = get_chat_name;
                    send_chat.user_name = get_name;
                    send_chat.chexk = "c";
                    db.Chat.Add(send_chat);
                }
                catch
                {
                    var get_store = get_section_store();
                    send_chat.customer_name = get_name;
                    send_chat.user_name = get_store;
                    send_chat.chexk = "u";
                    db.Chat.Add(send_chat);
                    try
                    {
                        var check_null = db.Alert_customer.Single(x => x.name == get_name && x.user_name == get_store);
                        check_null.date = DateTime.Today;
                        check_null.time = DateTime.Now.TimeOfDay;
                        check_null.alert = "show";

                    }
                    catch
                    {
                        alet_cu.alert = "show";
                        alet_cu.user_name = get_store;
                        alet_cu.name = get_name;
                        alet_cu.date = DateTime.Today;
                        alet_cu.time = DateTime.Now.TimeOfDay;
                        db.Alert_customer.Add(alet_cu);
                    }
                }
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult Alet_section()
        {
            var get_name = get_section_user();
            List<Alert_customer> alet_chat = new List<Alert_customer>();
            using (Order_Food_dbEntities1 db = new Order_Food_dbEntities1())
            {
                alet_chat = db.Alert_customer.Where(x => x.user_name == get_name).OrderByDescending(x => x.date).ThenByDescending(x => x.time).ToList();
                //foreach (var obj in db.Chat.Where(x => x.user_name == get_name))
                //{
                //}
            }
            return View(alet_chat);
        }
        [HttpPost]
        public ActionResult Alet_section(string get_chat_name, string get_non_alet, string get_check)
        {
            Session["get_chat_name"] = get_chat_name;
            Session["get_non_alet"] = get_non_alet;
            Session["get_check"] = get_check;

            return RedirectToAction("Homeaddstore", "UsingU");
        }
        string get_section_user()
        {
            try
            {
                string name = Session["user_user"].ToString();
                return (name);
            }
            catch
            {
                toHome();
            }
            return (null);
        }
        string get_section_store()
        {
            try
            {
                string name = Session["get_name_sto"].ToString();
                return (name);
            }
            catch
            {
                toHome();
            }
            return (null);
        }
    }
}