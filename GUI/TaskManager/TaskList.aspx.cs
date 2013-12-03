﻿using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using EbalitWebForms.BusinessLogicLayer;


namespace EbalitWebForms.GUI.TaskManager
{
    /// <summary>
    /// Code behind of TaskList.aspx
    /// </summary>
    public partial class TaskList : System.Web.UI.Page
    {
        /// <summary>
        /// Page Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if page is really requested (not postback)
            if (!IsPostBack)
            {
                //get filter parameters if the session parameter is available
                if (Session["FilterParams"] != null)
                {
                    var taskSearchDTO = (TaskSearchDTO)Session["FilterParams"];
                    txtDateFrom.Text = taskSearchDTO.DateFrom == new DateTime(1900, 1, 1) ? "" : taskSearchDTO.DateFrom.ToShortDateString();
                    txtDateTo.Text = taskSearchDTO.DateTo == new DateTime(2999, 12, 31) ? "" : taskSearchDTO.DateTo.ToShortDateString();
                    txtFreeText.Text = taskSearchDTO.Text;
                }

                //TODO: potentially remove... or rewrite
                ddlTaskStatus.SelectedIndex = 0;
            }
            lblStatus.Text = string.Empty;
        }

        /// <summary>
        /// Occurs when the Find link is clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkFind_Command(object sender, CommandEventArgs e)
        {
            //data binding requeries the datasource and therefore
            //fires the OnSelecting event (see below)
            lvwTasks.DataBind();
        }

        /// <summary>
        /// Executes before the Task ObjectDataSource calls the underlying get function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsTasks_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"] = GetTaskSearchDTO();
            e.InputParameters["orderByString"] = ViewState["orderByString"];
            e.InputParameters["sortDescending"] = ViewState["sortDescending"];
        }

        /// <summary>
        /// creates the search dto based on the user entry
        /// </summary>
        /// <returns></returns>
        private TaskSearchDTO GetTaskSearchDTO()
        {
            var taskSearchDTO = new TaskSearchDTO
            {
                DateFrom =
                    string.IsNullOrWhiteSpace(txtDateFrom.Text)
                        ? new DateTime(1900, 1, 1)
                        : Convert.ToDateTime(txtDateFrom.Text),
                DateTo =
                    string.IsNullOrWhiteSpace(txtDateTo.Text)
                        ? new DateTime(2999, 12, 31)
                        : Convert.ToDateTime(txtDateTo.Text),
                TaskCategoryId =
                    ddlTaskCategory.Items.GetSelectedItems()
                        .Select(cc => string.IsNullOrWhiteSpace(cc.Value) ? -1 : Convert.ToInt32(cc.Value))
                        .ToList(),
                TaskClosingType = ddlClosingType.Items.GetSelectedItems().Select(cc => cc.Text).ToList(),
                TaskPriority = ddlPriority.Items.GetSelectedItems().Select(cc => cc.Text).ToList(),
                TaskStatus = ddlTaskStatus.Items.GetSelectedItems().Select(cc => cc.Text).ToList(),
                Text = txtFreeText.Text
            };
            return taskSearchDTO;
        }

        

        /// <summary>
        /// resets all search fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkClear_Command(object sender, CommandEventArgs e)
        {
            Session["FilterParams"] = null;
            txtFreeText.Text = string.Empty;
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            ddlTaskCategory.SelectedIndex = -1;
            ddlClosingType.SelectedIndex = -1;
            ddlPriority.SelectedIndex = -1;
            ddlTaskStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// redirect to the details in insert mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCreate_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("/GUI/TaskManager/TaskDetail.aspx");
        }

        /// <summary>
        /// To cope with the us date bug.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwTasks_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            if (e.NewValues["DueDate"] != null)
                e.NewValues["DueDate"] = GUIHelper.GetUSDate(e.NewValues["DueDate"].ToString());
            //if actually not required but due to a bug where created on was deleted I need to check this here
            if (e.NewValues["CreatedOn"] != null)
                e.NewValues["CreatedOn"] = GUIHelper.GetUSDate(e.NewValues["CreatedOn"].ToString());
            e.NewValues["ChangedOn"] = DateTime.Now;
            e.NewValues["ChangedBy"] = Membership.GetUser().UserName;

        }

        /// <summary>
        /// redirects to the details of the selected entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDetails_Command(object sender, CommandEventArgs e)
        {
            Session.Add("FilterParams", GetTaskSearchDTO());
            Response.Redirect(string.Format("~/GUI/TaskManager/TaskDetail.aspx?Id={0}", e.CommandArgument));
        }

        /// <summary>
        /// Click on the Comments button updates the Comments datasource based on the id clicked and
        /// saves the id in the hidden field.
        /// The id is set as argument for the select method in the comments datasource.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnComments_Command(object sender, CommandEventArgs e)
        {
            hdfSelectedTaskId.Value = e.CommandArgument.ToString();
            lvwTaskComments.DataBind();

            //FindControl somehow was not working, so the recursive find was implemented
            var mpe = GUIHelper.RecursiveFindControl(lvwTasks, "ModalPopupExtender1");
            if (mpe != null)

                ((AjaxControlToolkit.ModalPopupExtender)mpe).Show();
        }

        /// <summary>
        /// make sure calculated fields are set on insert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwTaskComments_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            e.Values["FK_Task"] = hdfSelectedTaskId.Value;
            e.Values["CreatedOn"] = DateTime.Now;
            e.Values["CreatedBy"] = Membership.GetUser() != null ? Membership.GetUser().ToString() : "anonymous";
        }

        /// <summary>
        /// make sure calculated fields are set on update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwTaskComments_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            e.NewValues["FK_Task"] = hdfSelectedTaskId.Value;
            e.NewValues["ChangedOn"] = DateTime.Now;
            e.NewValues["ChangedBy"] = Membership.GetUser() != null ? Membership.GetUser().ToString() : "anonymous";
        }

        /// <summary>
        /// Callback checks whether deletion of data was successful, otherwise error is shown to user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsTasks_Deleted1(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                lblStatus.Text = "Deletion not possible. Make sure there are no task comments and try again.";
                e.ExceptionHandled = true;
            }
        }

        /// <summary>
        /// To highlight tasks which are due
        /// Based on code found here: http://forums.asp.net/t/1323404.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwTasks_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var task = (DataLayer.Task)e.Item.DataItem;
                if (task.DueDate != null && task.DueDate <= DateTime.Now && task.State != "Closed")
                {
                    var row = (HtmlTableRow)e.Item.FindControl("TaskListRow");
                    if (row != null)
                    {
                        row.Attributes.Add("class", "listviewHighlighted");
                    }
                }
            }
        }

        /// <summary>
        /// Create csv file from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkExport_Command(object sender, CommandEventArgs e)
        {
            var taskBLL = new TaskBLL();
            //Create the searchDTO according to the current field entries
            var searchDto = GetTaskSearchDTO();
            var csvObjs = taskBLL.GetFilteredTasksForCsv(searchDto);
            var data = CSVBuilder.ToCsv(";", csvObjs);
            FileDownloader.Data = data;
            FileDownloader.SendFileToClient();
        }

        protected void Filter_DataBound(object sender, EventArgs e)
        {
            if (Session["FilterParams"] != null)
            {
                var taskSearchDTO = (TaskSearchDTO)Session["FilterParams"];
                var senderDdl = (ListBox)sender;
                if (senderDdl != null)
                {
                    switch (senderDdl.ID)
                    {
                        case "ddlTaskCategory":
                            senderDdl.Items.SetSelectedItems(taskSearchDTO.TaskCategoryId);
                            break;
                        case "ddlClosingType":
                            senderDdl.Items.SetSelectedItems(taskSearchDTO.TaskClosingType);
                            break;
                        case "ddlPriority":
                            senderDdl.Items.SetSelectedItems(taskSearchDTO.TaskPriority);
                            break;
                        case "ddlTaskStatus":
                            senderDdl.Items.SetSelectedItems(taskSearchDTO.TaskStatus);
                            break;
                            //error - should not occur
                    }
                }

            }

        }


        /// <summary>
        /// Called by any orderby lnk Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkOrderBy_Command(object sender, CommandEventArgs e)
        {
            if (ViewState["orderByString"]!= null && ViewState["orderByString"].ToString() == e.CommandArgument.ToString())
            {
                ViewState["sortDescending"] = !(bool)ViewState["sortDescending"];
            }
            else
            {
                ViewState["sortDescending"] = false;
            }

            ViewState["orderByString"] = e.CommandArgument;
            lvwTasks.DataBind();
        }

    }
}