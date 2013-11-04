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

        }

        protected void odsResources_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = Convert.ToInt32(ddlProjects.SelectedValue);
        }


        /// <summary>
        /// TODO: continue here. Collapse does not work currently.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trvTask_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            this.txtTaskDropDown.Text = this.trvTask.SelectedNode.Text;
        }


    }
}