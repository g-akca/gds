//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroceryDeliverySystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.OrderItems = new HashSet<OrderItems>();
        }
    
        public int id { get; set; }
        public int userID { get; set; }
        public string status { get; set; }
        public decimal totalPrice { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public int storeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int cityID { get; set; }
        public int driverID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual Users Users { get; set; }
        public virtual Cities Cities { get; set; }
        public virtual Stores Stores { get; set; }
        public virtual Drivers Drivers { get; set; }
    }
}
