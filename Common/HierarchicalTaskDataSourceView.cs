using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.Common
{
    public class HierarchicalTaskDataSourceView:HierarchicalDataSourceView
    {
        private string _viewPath;

        private int _projectId;
        
        public HierarchicalTaskDataSourceView(string viewPath, string projectId )
        {
            
            _projectId = Convert.ToInt32(projectId);
            _viewPath = viewPath;
        }


        public override IHierarchicalEnumerable Select()
        {
            var repository = new WorkingReportBll();

            IList<ProjectTask> tasks = repository.GetTasks().ToList();

            if (!string.IsNullOrWhiteSpace(_viewPath))
            {
                tasks = tasks.Where(cc => cc.Parent == Guid.Parse(_viewPath)).ToList();
            }
            else
            {
                tasks = tasks.Where(cc => cc.Parent == cc.ProjectProject.Guid).ToList();
            }
            return new HierarchicalTaskEnumerable(tasks, _viewPath);
        }
    }
}