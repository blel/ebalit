using EbalitWebForms.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer.WorkingReport
{
    public class ProjectBll: Repository<ProjectProject>
    {
        public bool IsUnique(string projectName, int id)
        {
            return !EbalitDbContext.ProjectProjects.Any(cc => cc.Name == projectName && cc.Id != id);
        }
    }
}