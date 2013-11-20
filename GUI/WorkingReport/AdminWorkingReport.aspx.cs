using System;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class AdminWorkingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsAvailableResources_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (ddlProjects.SelectedItem != null)
            {
                e.InputParameters["projectId"]
                  = ddlProjects.SelectedItem.Value;
            }
        }

        protected void ddlUsers_TextChanged(object sender, EventArgs e)
        {
            lsbAssignedUsers.DataBind();
        }

        protected void ddlProjects_TextChanged(object sender, EventArgs e)
        {
            lsbAvailableResources.DataBind();
        }

        protected void odsAssignedResources_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (ddlUsers.SelectedItem != null)
            {
                e.InputParameters["userName"] = ddlUsers.SelectedItem.Value;
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            var selectedResource = lsbAvailableResources.SelectedValue;

            var selectedUser = ddlUsers.SelectedValue;

            if (!string.IsNullOrWhiteSpace(selectedResource) && !string.IsNullOrWhiteSpace(selectedUser))
            {

                var workingReportBll = new WorkingReportBll();

                workingReportBll.AssignUser(selectedUser, Convert.ToInt32(selectedResource));

                lsbAssignedUsers.DataBind();

                lsbAvailableResources.DataBind();

 
            }
        }

        /// <summary>
        /// Remove assingment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            var selectedResource = lsbAssignedUsers.SelectedValue;

            var selectedUser = ddlUsers.SelectedValue;

            if (!string.IsNullOrWhiteSpace(selectedResource ) && !string.IsNullOrWhiteSpace(selectedUser))
            {
                var workingReportBll = new WorkingReportBll();

                workingReportBll.RemoveUser(selectedUser, Convert.ToInt32(selectedResource));

                lsbAssignedUsers.DataBind();

                lsbAvailableResources.DataBind();

            }
        }
    }
}