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
    
    public partial class ProjectProject
    {
        public ProjectProject()
        {
            this.ProjectResources = new HashSet<ProjectResource>();
            this.ProjectTasks = new HashSet<ProjectTask>();
            this.ProjectWorkingReports = new HashSet<ProjectWorkingReport>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.Guid Guid { get; set; }
    
        public virtual ICollection<ProjectResource> ProjectResources { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<ProjectWorkingReport> ProjectWorkingReports { get; set; }
    }
}