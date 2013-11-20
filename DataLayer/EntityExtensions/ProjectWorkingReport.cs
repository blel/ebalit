using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.BusinessLogicLayer.DTO;

namespace EbalitWebForms.DataLayer
{
    public partial class ProjectWorkingReport
    {
        public WorkingReportToCsvDto ToCsvDto()
        {
            return new WorkingReportToCsvDto
            {
                Comments = Notes,
                To = Convert.ToDateTime(To).Hour + ":" + Convert.ToDateTime(To).Minute,
                From = Convert.ToDateTime(From).Hour + ":" + Convert.ToDateTime(From).Minute,
                Date = string.Format("{0:d}", Convert.ToDateTime(From)),
                Project = ProjectProject.Name,
                Resource = ProjectResource.Name,
                Task = ProjectTask.Name,
                Total = Convert.ToString(Total)
            };
        }

    }
}