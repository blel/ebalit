using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.BusinessLogicLayer.CsvFileImport;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer.WorkingReport
{
    public class ErroneousWorkingReportBll:Repository<ErroneousWorkingReport>
    {
        public void PersistErroneousWorkingReports(IList<WorkingReportCsvFile> erroneousWorkingReports)
        {
            foreach (var erroneousWorkingReport in erroneousWorkingReports)
            {
                var erroneousWorkingReportEntity = new ErroneousWorkingReport
                {
                    Date = erroneousWorkingReport.Date,
                    Description = erroneousWorkingReport.Description,
                    ProjectName = erroneousWorkingReport.ProjectName,
                    ResourceName = erroneousWorkingReport.ResourceName,
                    TfsTaskId = erroneousWorkingReport.TfsTaskId,
                    WorkingTime = erroneousWorkingReport.WorkingTime
                };
                EbalitDbContext.ErroneousWorkingReports.Add(erroneousWorkingReportEntity);
            }
            EbalitDbContext.SaveChanges();
        }  
 
        
    }
}