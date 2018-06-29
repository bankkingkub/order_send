using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
//using System.Web.UI.WebControls;

namespace Order_Food.Models
{
    public class Add_StoreMetadata
    {
        public int Food_Picture_id { get; set; }
        public string C_FK_Customer_id { get; set; }
        public string Food_Picture_pic { get; set; }
        public string Food_Picture_name { get; set; }
    }
    [MetadataType(typeof(Add_StoreMetadata))]
    public class add_store
    {
        public int User_id { get; set; }
        public string User_user { get; set; }
        public string User_pw { get; set; }
        public string User_repw { get; set; }
        public string User_name { get; set; }
        public string User_last_name { get; set; }
        public Nullable<int> C_FK_Address_U { get; set; }
        public Nullable<int> C_FK_Location_U_id { get; set; }
        public string User_phone { get; set; }
        public string Status { get; set; }
        public string Province { get; set; }
    }
}