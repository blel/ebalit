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
    
    public partial class Task
    {
        public Task()
        {
            this.TaskComments = new HashSet<TaskComment>();
        }
    
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> FK_TaskCategory { get; set; }
        public string State { get; set; }
        public string Priority { get; set; }
        public string ClosingType { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    
        public virtual TaskCategory TaskCategory { get; set; }
        public virtual ICollection<TaskComment> TaskComments { get; set; }
    }
}
