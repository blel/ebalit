using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer.CsvFileImport
{
    public class WorkingReportCsvFile
    {
        public string Date { get; set; }

        public string WorkingTime { get; set; }

        public string Description { get; set; }

        public string TfsTaskName { get; set; }
    }
}