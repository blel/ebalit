using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.Common
{
    public delegate void Selecting(object sender, ObjectDataSourceSelectingEventArgs e);
    
    public class HierarchicalTaskDataSource : HierarchicalDataSourceControl
    {
       
        public event Selecting Selecting;
        
        public string ProjectId { get; set; }

        public HierarchicalTaskDataSource()
        {

        }

        protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
        {
            var inputArgs = new OrderedDictionary();
            
            inputArgs.Add("ProjectId", ProjectId);

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