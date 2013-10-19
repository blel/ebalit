using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EbalitWebForms.DataLayer.Controlling
{
    public class ControllingContext:DbContext
    {
        public ControllingContext() : base() { }

        public DbSet<Project> Projects { get; set; }
    }
}