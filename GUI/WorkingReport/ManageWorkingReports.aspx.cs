using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class ManageWorkingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: This seems to work, but need to checkout why exactly...
            scmAjaxToolkit.RegisterPostBackControl(trvTask);
        }

        /// <summary>
        /// When resources are selected from the db,
        /// it is checked whether there is a particular project selected, for which the resources 
        /// should be displayed. If none is selected, nothing will be shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsResources_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ddlProjects.SelectedValue))
            {
                e.InputParameters["projectId"] = Convert.ToInt32(ddlProjects.SelectedValue);
            }
        }


        /// <summary>
        /// Assign selected text to the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trvTask_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            txtTaskDropDown.Text = trvTask.SelectedNode.Text;
        }

        protected void lnkCreate_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("/GUI/WorkingReport/CreateWorkingReport.aspx");
        }


    }
}