using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Order_Food.Class
{
    public partial class Register
    {
        public int User_id { get; set; }
        public string User_user { get; set; }
        [DataType(DataType.Password)]
        public string User_pw { get; set; }
        [DataType(DataType.Password)]
        public string User_repw { get; set; }
        public string User_name { get; set; }
        public string User_last_name { get; set; }
        public Nullable<int> C_FK_Address_U { get; set; }
        public Nullable<int> C_FK_Location_U_id { get; set; }
        public string User_phone { get; set; }
    }
    public class UploadFileModel
    {
        public UploadFileModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public List<HttpPostedFileBase> Files { get; set; }
        public string FirstName { get; set; }
    }
}