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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Add_Store
    {
        public int id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string pic { get; set; }
        [NotMapped]
        public List<Category> category2 { get; set; }
        public string numcut { get; set; }
    }
}
