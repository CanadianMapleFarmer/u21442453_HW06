//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace u21442453_HW06.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public orders()
        {
            this.order_items = new HashSet<order_items>();
        }
    
        public int order_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public byte order_status { get; set; }
        public System.DateTime order_date { get; set; }
        public System.DateTime required_date { get; set; }
        public Nullable<System.DateTime> shipped_date { get; set; }
        public int store_id { get; set; }
        public int staff_id { get; set; }
    
        public virtual customers customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_items> order_items { get; set; }
        public virtual staffs staffs { get; set; }
        public virtual stores stores { get; set; }
    }
}
