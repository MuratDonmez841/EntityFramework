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
    
    public partial class FeedBackDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeedBackDetail()
        {
            this.FeedBack_FeedBackDetail = new HashSet<FeedBack_FeedBackDetail>();
        }
    
        public int FeedBackDetail_ID { get; set; }
        public Nullable<int> DısGörünüs { get; set; }
        public Nullable<int> KullanımKolaylıgı { get; set; }
        public Nullable<int> FiyatPerf { get; set; }
        public Nullable<int> Kalite { get; set; }
        public Nullable<int> Dayanıklılık { get; set; }
        public string UserReview { get; set; }
        public string CompanyReview { get; set; }
        public Nullable<byte> Flag { get; set; }
        public string IMG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedBack_FeedBackDetail> FeedBack_FeedBackDetail { get; set; }
    }
}
