//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EbalitWebForms.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Wine
    {
        public Wine()
        {
            this.WineToWineAttributes = new HashSet<WineToWineAttribute>();
            this.WineConsumations = new HashSet<WineConsumation>();
        }
    
        public int Id { get; set; }
        public string Label { get; set; }
        public int Year { get; set; }
        public Nullable<System.DateTime> BoughtOn { get; set; }
        public string Magazine { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string ChangedBy { get; set; }
        public string Remarks { get; set; }
        public string Grape { get; set; }
        public string Origin { get; set; }
    
        public virtual ICollection<WineToWineAttribute> WineToWineAttributes { get; set; }
        public virtual ICollection<WineConsumation> WineConsumations { get; set; }
    }
}
