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
    
    public partial class Stores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stores()
        {
            this.Categories = new HashSet<Categories>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int cityID { get; set; }
        public string imageURL { get; set; }
        public string description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categories> Categories { get; set; }
        public virtual Cities Cities { get; set; }
    }
}
