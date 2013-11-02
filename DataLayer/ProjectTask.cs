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
    
    public partial class ProjectTask
    {
        public ProjectTask()
        {
            this.ProjectResourceTaskAssignments = new HashSet<ProjectResourceTaskAssignment>();
            this.ProjectWorkingReports = new HashSet<ProjectWorkingReport>();
        }
    
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> Parent { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<double> ActualWork { get; set; }
    
        public virtual ProjectProject ProjectProject { get; set; }
        public virtual ICollection<ProjectResourceTaskAssignment> ProjectResourceTaskAssignments { get; set; }
        public virtual ICollection<ProjectWorkingReport> ProjectWorkingReports { get; set; }
    }
}
