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
    
    public partial class BlogCategory
    {
        public BlogCategory()
        {
            this.BlogEntries = new HashSet<BlogEntry>();
        }
    
        public int Id { get; set; }
        public string Category { get; set; }
        public int FK_Topic { get; set; }
    
        public virtual ICollection<BlogEntry> BlogEntries { get; set; }
        public virtual BlogTopic BlogTopic { get; set; }
    }
}
