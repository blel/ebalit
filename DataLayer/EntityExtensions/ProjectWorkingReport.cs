using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.BusinessLogicLayer.DTO;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;

namespace EbalitWebForms.DataLayer
{
    public partial class ProjectWorkingReport
    {
        public WorkingReportToCsvDto ToCsvDto()
        {
            var workingReportBll = new WorkingReportBll();

            return new WorkingReportToCsvDto
            {
                Comments = Notes,
                To = Convert.ToDateTime(To).Hour + ":" + Convert.ToDateTime(To).Minute,
                From = Convert.ToDateTime(From).Hour + ":" + Convert.ToDateTime(From).Minute,
                Date = string.Format("{0:d}", Convert.ToDateTime(From)),
                Project = ProjectProject.Name,
                Resource = ProjectResource.Name,
                TfsTaskId = ProjectTask.TfsTaskId,
                Task = workingReportBll.GetTaskPath(ProjectTask.TfsTaskId,ProjectProject.Id),
                Total = Convert.ToString(Total)
            };
        }

    }
}