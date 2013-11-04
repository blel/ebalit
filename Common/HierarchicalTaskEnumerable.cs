using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.Common
{
    public class HierarchicalTaskEnumerable:IHierarchicalEnumerable
    {
        private readonly IList<ProjectTask> _tasks;

        private string _viewPath;
        
        public HierarchicalTaskEnumerable(IList<ProjectTask> tasks, string viewPath)
        {
            _tasks = tasks;

            _viewPath = viewPath;
        }

        public IEnumerator GetEnumerator()
        {
            return _tasks.GetEnumerator();
        }

        public IHierarchyData GetHierarchyData(object enumeratedItem)
        {
            return new HierarchyTaskData((ProjectTask)enumeratedItem);
        }
    }
}