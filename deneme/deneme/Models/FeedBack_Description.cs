//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace deneme.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FeedBack_Description
    {
        public Nullable<int> FeedBackID { get; set; }
        public string Description { get; set; }
        public int Desc_ID { get; set; }
        public string CompanyDescription { get; set; }
    
        public virtual FeedBack_Info FeedBack_Info { get; set; }
    }
}