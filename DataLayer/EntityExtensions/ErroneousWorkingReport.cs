using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.BusinessLogicLayer.CsvFileImport;

namespace EbalitWebForms.DataLayer
{
    public partial class ErroneousWorkingReport
    {
        public WorkingReportCsvFile ToCsvFile()
        {
            return new WorkingReportCsvFile
            {
                TfsTaskId = TfsTaskId,
                ProjectName = ProjectName,
                Description = Description,
                ResourceName = ResourceName,
                WorkingTime = Convert.ToDecimal(WorkingTime),
                Date = Convert.ToDateTime(Date)
            };
        }
    }
}