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
    
    public partial class WineAttribute
    {
        public WineAttribute()
        {
            this.WineToWineAttributes = new HashSet<WineToWineAttribute>();
        }
    
        public int Id { get; set; }
        public string Attribute { get; set; }
        public string Remarks { get; set; }
    
        public virtual ICollection<WineToWineAttribute> WineToWineAttributes { get; set; }
    }
}
