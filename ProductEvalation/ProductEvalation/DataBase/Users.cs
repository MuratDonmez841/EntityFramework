//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductEvalation.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.FeedBack = new HashSet<FeedBack>();
            this.Sells = new HashSet<Sells>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public Nullable<long> UserTel { get; set; }
        public string UserMail { get; set; }
        public string UserProvi { get; set; }
        public string UserDisct { get; set; }
        public Nullable<int> UserPostaKod { get; set; }
        public string UserAdress { get; set; }
        public string UserPassword { get; set; }
        public string profilPhoto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedBack> FeedBack { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sells> Sells { get; set; }
    }
}
