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
    
    public partial class WineToWineAttribute
    {
        public int Id { get; set; }
        public int FK_Wine { get; set; }
        public int FK_WineAttribute { get; set; }
    
        public virtual Wine Wine { get; set; }
        public virtual WineAttribute WineAttribute { get; set; }
    }
}
