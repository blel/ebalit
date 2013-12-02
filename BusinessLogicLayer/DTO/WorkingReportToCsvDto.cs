using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer.DTO
{
    public class WorkingReportToCsvDto
    {
        public string Project { get; set; }

        public string Resource { get; set; }

        public string TfsTaskId { get; set; }

        public string Task { get; set; }

        public string Date { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Total { get; set; }

        public string Comments { get; set; }

    }
}