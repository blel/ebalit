using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.TaskManager
{
    public partial class TaskDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.dtvTask.ChangeMode(DetailsViewMode.Insert);
                this.dtvTask.FindControl("txtSubject").Focus();
                InsertEmptyItem("ddlTaskCategory");

            }


        }

        protected void xdsTaskStatus_DataBinding(object sender, EventArgs e)
        {

        }

        protected void odsTaskCategories_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {


    
        }


        private void InsertEmptyItem(string ddlName)
        {
            DropDownList ddlTaskCategory = (DropDownList)dtvTask.FindControl(ddlName);
            if (ddlTaskCategory != null)
            {
                ddlTaskCategory.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                ddlTaskCategory.SelectedIndex = 0;
            }
        }

    }

    
}