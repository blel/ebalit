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
        public IList<int> TaskCategoryId { get; set; }
        public IList<string> TaskStatus { get; set; }
        public IList<string> TaskPriority { get; set; }
        public IList<string> TaskClosingType { get; set; }
    }
}