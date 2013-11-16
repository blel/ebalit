using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.Common
{
    public delegate void Selecting(object sender, ObjectDataSourceSelectingEventArgs e);
    
    public class HierarchicalTaskDataSource : HierarchicalDataSourceControl
    {
       
        public event Selecting Selecting;
        
        public string ProjectId { get; set; }


        protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
        {
            var inputArgs = new OrderedDictionary {{"ProjectId", ProjectId}};

            var selectArgs = new DataSourceSelectArguments();

            var eventArgs = new ObjectDataSourceSelectingEventArgs(inputArgs,selectArgs,false);
            if (Selecting != null)
            {
                Selecting(this, eventArgs);
            }
            ProjectId = Convert.ToString(eventArgs.InputParameters["ProjectId"]);
            
           return new HierarchicalTaskDataSourceView(viewPath, ProjectId);      
        }
    }

}