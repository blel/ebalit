using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.Common
{
    public class HierarchicalTaskDataSource : HierarchicalDataSourceControl
    {
       
        public string ProjectId { get; set; }

        public HierarchicalTaskDataSource()
        {

        }

        protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
        {
           return new HierarchicalTaskDataSourceView(viewPath, ProjectId);
        }

     


    }

}