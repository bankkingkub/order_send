//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Order_Food.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public int C_PK_Menu_id { get; set; }
        public Nullable<int> C_FK_Customer_id { get; set; }
        public string Menu_time { get; set; }
        public string Menu_text { get; set; }
        public Nullable<int> Menu_price { get; set; }
        public Nullable<int> Menu_amount { get; set; }
    }
}
