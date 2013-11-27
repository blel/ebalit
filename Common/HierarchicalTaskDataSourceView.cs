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
        private readonly string _viewPath;

        private readonly int _projectId;
        
        public HierarchicalTaskDataSourceView(string viewPath, string projectId)
        {
            if (!string.IsNullOrWhiteSpace(projectId))
            {
                _projectId = Convert.ToInt32(projectId);
            }
            _viewPath = viewPath;
        }


        public override IHierarchicalEnumerable Select()
        {
            var repository = new WorkingReportBll();

            //retrieve the tasks for the project with given id
            IList<ProjectTask> tasks = repository.GetTasks(_projectId).ToList();

            //if a view path is given, return all tasks with that view path as parent
            if (!string.IsNullOrWhiteSpace(_viewPath))
            {
                tasks = tasks.Where(cc => cc.ParentTfsTaskId == _viewPath).ToList();
            }
            else
            {
                //otherwise return all root tasks (parentTfsTaskId is empty
                tasks = tasks.Where(cc => string.IsNullOrWhiteSpace(cc.ParentTfsTaskId)).ToList();
            }
            return new HierarchicalTaskEnumerable(tasks, _viewPath);
        }
    }
}