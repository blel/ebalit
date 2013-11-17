using System;
using System.Collections.Generic;
using System.Globalization;
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

        /// <summary>
        /// Save the Guid of the selected task in view state for later use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            var cultureInfo = new CultureInfo("en-US");

            var date = DateTime.Parse( GUIHelper.GetUSDate(e.Values["From"].ToString()), cultureInfo.DateTimeFormat);


            e.Values["From"] = new DateTime(date.Year, date.Month, date.Day, fromTime.Hour, fromTime.Minute, 0);
            e.Values["To"] = new DateTime(date.Year, date.Month, date.Day, toTime.Hour, toTime.Minute, 0);

            //manual "data binding" 
            var ddlResource = (DropDownList) dtvCreateWorkingReport.FindControl("ddlResource");
            if (ddlResource != null && ddlResource.SelectedItem!= null)
            {
                e.Values["ResourceId"] = ddlResource.SelectedItem.Value;
            }
        }

        /// <summary>
        /// Before updating a working report,
        /// make sure all parameters are setup correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvCreateWorkingReport_OnItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            //need to get the id of the selected task
            e.NewValues["TaskId"] = new WorkingReportBll().GetTaskIdByGuid(Guid.Parse(ViewState["selectedTaskId"].ToString()));

            var fromTime = ((WebUserControls.TimeControl)
                dtvCreateWorkingReport.FindControl("FromTime")).DisplayTime;

            var toTime = ((WebUserControls.TimeControl)
                dtvCreateWorkingReport.FindControl("ToTime")).DisplayTime;

            var cultureInfo = new CultureInfo("en-US");

            var date = DateTime.Parse(GUIHelper.GetUSDate(e.OldValues["From"].ToString()), cultureInfo.DateTimeFormat);


            e.NewValues["From"] = new DateTime(date.Year, date.Month, date.Day, fromTime.Hour, fromTime.Minute, 0);
            e.NewValues["To"] = new DateTime(date.Year, date.Month, date.Day, toTime.Hour, toTime.Minute, 0);

            //manual "data binding" 
            var ddlResource = (DropDownList)dtvCreateWorkingReport.FindControl("ddlResource");
            if (ddlResource != null && ddlResource.SelectedItem != null)
            {
                e.NewValues["ResourceId"] = ddlResource.SelectedItem.Value;
            }

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
        /// Reload ddlResource and trvTask if ddlProject changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPRoject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlResource = (DropDownList)(dtvCreateWorkingReport.FindControl("ddlResource"));
            if (ddlResource != null)
            {
                ddlResource.DataBind();
                trvTask.DataBind();
            }
        }

        /// <summary>
        /// Make sure only tasks from the selected project are shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void htsTasks_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            var ddlProject = (DropDownList) dtvCreateWorkingReport.FindControl("ddlProject");
            if (ddlProject != null)
            {
                e.InputParameters["ProjectId"] = ddlProject.SelectedValue;
            }
        }

        /// <summary>
        /// Make sure the id from the query string is taken, especially 
        /// when editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsWorkingReport_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                e.InputParameters["id"] = Request.QueryString["Id"];
            }
        }

        /// <summary>
        /// SEt the task text 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvCreateWorkingReport_OnDataBound(object sender, EventArgs e)
        {
            var txtTask = dtvCreateWorkingReport.FindControl("txtTask");

            if (txtTask != null && Request.QueryString["Id"] != null)
            {
                var bll = new WorkingReportBll();
                var workingReport = bll.GetWorkingReport(Convert.ToInt32(Request.QueryString["Id"]));

                ((TextBox) txtTask).Text = workingReport.ProjectTask.Name;
                //store the Guid in the view state
                ViewState.Add("selectedTaskId", workingReport.ProjectTask.Guid);
            }
        }

        /// <summary>
        /// return back to the list screen when pressing cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvCreateWorkingReport_OnModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit)
            {
                e.Cancel = true;
                Response.Redirect("/GUI/WorkingReport/ManageWorkingReports.aspx");
            }
        }

        /// <summary>
        /// link back to list screen after update and insert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvCreateWorkingReport_OnItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            Response.Redirect("/GUI/WorkingReport/ManageWorkingReports.aspx");
        }

        /// <summary>
        /// link back to list screen after update and insert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvCreateWorkingReport_OnItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            Response.Redirect("/GUI/WorkingReport/ManageWorkingReports.aspx");
        }
    }
}