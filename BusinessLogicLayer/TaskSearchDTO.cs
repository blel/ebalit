using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class TaskSearchDTO
    {
        public string Text { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int TaskCategoryId { get; set; }
        public string TaskStatus { get; set; }
        public string TaskPriority { get; set; }
        public string TaskClosingType { get; set; }
    }
}