using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Configuration.Interfaces;

namespace EbalitWebForms.BusinessLogicLayer.WorkingReport
{
    public class WorkingReportConfigurationSection:IConfigurationSection
    {
        public bool DeleteResourcesIfRemovedFromProject { get; set; }

        public bool DeleteTasksIfRemovedFromProject { get; set; }
    }
}