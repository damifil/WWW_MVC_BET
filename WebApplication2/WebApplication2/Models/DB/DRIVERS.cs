//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class DRIVERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DRIVERS()
        {
            this.PARTICIPANTS = new HashSet<PARTICIPANTS>();
        }
    
        public int Driver_ID { get; set; }
        public string Driver_Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PARTICIPANTS> PARTICIPANTS { get; set; }
    }
}