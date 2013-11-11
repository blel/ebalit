using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class CreateWorkingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.scmAjaxToolkit.RegisterPostBackControl(trvTask);
            //if site is really loaded and no postback
            if (!IsPostBack)
            {

                //if there is a "Id" parameter in the url change to edit mode
                if (Request.QueryString["Id"] != null)
                {
                    dtvCreateWorkingReport.ChangeMode(DetailsViewMode.Edit);
                }
                //otherwise go to insert mode
                else
                {
                    dtvCreateWorkingReport.ChangeMode(DetailsViewMode.Insert);
                }
            }
        }

        protected void ddlPRoject_OnTextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        protected void odsTasks_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            var id = Convert.ToInt32(((DropDownList)dtvCreateWorkingReport.FindControl("ddlProject")).SelectedValue);
            e.InputParameters["project"] = new WorkingReportBll().GetProject(id);
        }


        protected void trvTask_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            var taskTextBox = (TextBox)GUIHelper.RecursiveFindControl(this, "txtTask");
            if (taskTextBox != null)
            {
                taskTextBox.Text = trvTask.SelectedValue;
                ViewState.Add("selectedTaskId", trvTask.SelectedNode.DataPath);
            }
        }

        /// <summary>
        /// Before inserting a new working report,
        /// make sure all parameters are setup correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvCreateWorkingReport_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //need to get the id of the selected task
            e.Values["TaskId"] = new WorkingReportBll().GetTaskIdByGuid(Guid.Parse(ViewState["selectedTaskId"].ToString()));

            var fromTime = ((WebUserControls.TimeControl)
                dtvCreateWorkingReport.FindControl("FromTime")).DisplayTime;

            var toTime = ((WebUserControls.TimeControl)
                dtvCreateWorkingReport.FindControl("ToTime")).DisplayTime;

            var date = DateTime.Parse(GUIHelper.GetUSDate(e.Values["From"].ToString()));


            e.Values["From"] = new DateTime(date.Year, date.Month, date.Day, fromTime.Hour, fromTime.Minute, 0);
            e.Values["To"] = new DateTime(date.Year, date.Month, date.Day, toTime.Hour, toTime.Minute, 0);
            e.Values["ResourceId"] = ((ListItem)e.Values["ResourceId"]).Value;

        }

        /// <summary>
        /// when loading resources, get selected project id from project ddl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsResource_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            var ddlProject = (DropDownList)(dtvCreateWorkingReport.FindControl("ddlProject"));
            if (ddlProject != null)
            {
                e.InputParameters["projectId"] = ddlProject.SelectedItem.Value;
            }
        }

        /// <summary>
        /// Reload ddlResource if ddlProject changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPRoject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlResource = (DropDownList)(dtvCreateWorkingReport.FindControl("ddlResource"));
            if (ddlResource != null)
            {
                ddlResource.DataBind();
            }
        }
    }
}