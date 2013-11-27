using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.Common
{
    /// <summary>
    /// Delegate used for teh OnSelectign event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Selecting(object sender, ObjectDataSourceSelectingEventArgs e);
    
    /// <summary>
    /// Implementation of the hierarchical data source
    /// </summary>
    public class HierarchicalTaskDataSource : HierarchicalDataSourceControl
    {
       
        /// <summary>
        /// The selecting event
        /// </summary>
        public event Selecting Selecting;
        
        /// <summary>
        /// The project id to for which the tasks shall be returned as hierarchical datasource
        /// </summary>
        public string ProjectId { get; set; }


        protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
        {
            //setup the event args for the selecting event
            var inputArgs = new OrderedDictionary {{"ProjectId", ProjectId}};
            var selectArgs = new DataSourceSelectArguments();
            var eventArgs = new ObjectDataSourceSelectingEventArgs(inputArgs, selectArgs, false);
            
            //call the selcting event if there are subscribers
            if (Selecting != null)
            {
                Selecting(this, eventArgs);
            }

            //assign the project id from the subscriber
            ProjectId = Convert.ToString(eventArgs.InputParameters["ProjectId"]);
           
 
           return new HierarchicalTaskDataSourceView(viewPath, ProjectId);      
        }
    }

}