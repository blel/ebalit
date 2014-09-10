using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvParser;

namespace EbalitWebForms.BusinessLogicLayer.CsvFileImport
{
    public class WorkingReportCsvFile
    {
        [MandatoryField]
        public string ProjectName { get; set; }
        
        [MandatoryField]
        public string ResourceName { get; set; }

        [MandatoryField]
        public DateTime Date { get; set; }

        [MandatoryField]
        public decimal WorkingTime { get; set; }

        [MandatoryField]
        public string TfsTaskId { get; set; }

        public string Description { get; set; }
    }
}