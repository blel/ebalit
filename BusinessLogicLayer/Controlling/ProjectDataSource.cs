using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EbalitWebForms.BusinessLogicLayer.Controlling
{
    public class ProjectDataSource:IHierarchicalDataSource
    {
        private HierarchicalDataSourceView _dsv = new ProjectDataSourceView();
        private ControllingElement _theTree;

        public ProjectDataSource()
        {
            _theTree = new Project("X-Elisa", "XE");


            PSPElement child1 = new PSPElement("Architecture");
            _theTree.Add(child1);

            _theTree.Add(new PSPElement("another child"));
            child1.Add(new PSPElement("and the last"));
            
        }

        
        
        public event EventHandler DataSourceChanged;

        public HierarchicalDataSourceView GetHierarchicalView(string viewPath)
        {
            return _theTree;
        }
    }
}