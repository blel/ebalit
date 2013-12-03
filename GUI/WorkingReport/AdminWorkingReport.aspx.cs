﻿using System;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;
using EbalitWebForms.Common;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class AdminWorkingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var manager = Configuration.ConfigurationManager.GetManager();
                var config = manager.LoadConfiguration();

                var section = config.GetConfigurationSection<WorkingReportConfigurationSection>();
                chkDeleteResources.Checked = section.DeleteResourcesIfRemovedFromProject;
                chkDeleteTasks.Checked = section.DeleteTasksIfRemovedFromProject;
            }
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

                try
                {
                    workingReportBll.AssignUser(selectedUser, Convert.ToInt32(selectedResource));
                }
                catch (InvalidOperationException ex)
                {
                    StatusBar.StatusText = ex.Message;
                }
                
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

                try
                {

                    workingReportBll.RemoveUser(selectedUser, Convert.ToInt32(selectedResource));
                }
                catch (InvalidOperationException ex)
                {
                    StatusBar.StatusText = ex.Message;
                }

                lsbAssignedUsers.DataBind();

                lsbAvailableResources.DataBind();

            }
        }



        protected void lnkSave_OnCommand(object sender, CommandEventArgs e)
        {
            var manager = Configuration.ConfigurationManager.GetManager();

            var section = new WorkingReportConfigurationSection
            {
                DeleteResourcesIfRemovedFromProject = chkDeleteResources.Checked,

                DeleteTasksIfRemovedFromProject = chkDeleteTasks.Checked
            };

            var config = manager.LoadConfiguration();
            config.AddConfigurationSection(section);
            manager.SaveConfiguration();
        }
    }
}