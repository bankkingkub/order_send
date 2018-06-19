﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace Order_Food.Models
{
    public class Add_UserMetadata
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
        public string Status { get; set; }
    }
}