using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class TaskToCsvDTO
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string DueDate { get; set; }
        public string TaskCategory { get; set; }
        public string State { get; set; }
        public string Priority { get; set; }
        public string ClosingType { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ChangedOn { get; set; }
        public string ChangedBy { get; set; }
        public string Comments { get; set; }
    }
}