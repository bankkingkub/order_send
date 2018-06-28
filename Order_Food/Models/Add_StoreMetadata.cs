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
        public int id { get; set; }
        [Required]
        public string description { get; set; }
        public string name { get; set; }
        public string pic { get; set; }
        public string getname { get; set; }
    }

}