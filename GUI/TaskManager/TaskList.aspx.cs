using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Web.Security;
using EbalitWebForms.BusinessLogicLayer;


namespace EbalitWebForms.GUI.TaskManager
{
    public partial class TaskList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlTaskStatus.SelectedIndex = 1;
            }
            this.lblStatus.Text = string.Empty;
        }

        protected void lnkFind_Command(object sender, CommandEventArgs e)
        {
            this.lvwTasks.DataBind();
        }

        protected void odsTasks_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"] = GetTaskSearchDTO();
        }

        private BusinessLogicLayer.TaskSearchDTO GetTaskSearchDTO()
        {
            BusinessLogicLayer.TaskSearchDTO taskSearchDTO = new BusinessLogicLayer.TaskSearchDTO();
            taskSearchDTO.DateFrom = string.IsNullOrWhiteSpace(txtDateFrom.Text) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(this.txtDateFrom.Text);
            taskSearchDTO.DateTo = string.IsNullOrWhiteSpace(txtDateTo.Text) ? new DateTime(2999, 12, 31) : Convert.ToDateTime(this.txtDateTo.Text);
            taskSearchDTO.TaskCategoryId = string.IsNullOrWhiteSpace(ddlTaskCategory.Text) ? 0 : Convert.ToInt32(this.ddlTaskCategory.SelectedValue);
            taskSearchDTO.TaskClosingType = Convert.ToString(this.ddlClosingType.SelectedItem);
            taskSearchDTO.TaskPriority = Convert.ToString(this.ddlPriority.SelectedItem);
            taskSearchDTO.TaskStatus = Convert.ToString(this.ddlTaskStatus.SelectedItem);
            taskSearchDTO.Text = this.txtFreeText.Text;
            return taskSearchDTO;
        }

        protected void lnkClear_Command(object sender, CommandEventArgs e)
        {
            this.txtFreeText.Text = string.Empty;
            this.txtDateFrom.Text = string.Empty;
            this.txtDateTo.Text = string.Empty;
            this.ddlTaskCategory.SelectedIndex = 0;
            this.ddlClosingType.SelectedIndex = 0;
            this.ddlPriority.SelectedIndex = 0;
            this.ddlTaskStatus.SelectedIndex = 0;
        }

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
        }

        protected void btnDetails_Command(object sender, CommandEventArgs e)
        {
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
            this.hdfSelectedTaskId.Value = e.CommandArgument.ToString();
            this.lvwTaskComments.DataBind();

            //FindControl somehow was not working, so the recursive find was implemented
            var mpe = GUIHelper.RecursiveFindControl(this.lvwTasks, "ModalPopupExtender1");
            if (mpe != null)

                ((AjaxControlToolkit.ModalPopupExtender)mpe).Show();
        }



        protected void odsTaskComments_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwTaskComments_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            e.Values["FK_Task"] = hdfSelectedTaskId.Value;
            e.Values["CreatedOn"] = DateTime.Now;
            e.Values["CreatedBy"] = Membership.GetUser() != null ? Membership.GetUser().ToString() : "anonymous";
        }

        protected void lvwTaskComments_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            e.NewValues["FK_Task"] = hdfSelectedTaskId.Value;
            e.NewValues["ChangedOn"] = DateTime.Now;
            e.NewValues["ChangedBy"] = Membership.GetUser() != null ? Membership.GetUser().ToString() : "anonymous";
        }

        protected void odsTasks_Deleted1(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                this.lblStatus.Text = "Deletion not possible. Make sure there are no task comments and try again.";
                e.ExceptionHandled = true;

            }

        }

        /// <summary>
        /// To highlight tasks which are due
        /// Based on code found here: http://forums.asp.net/t/1323404.aspx
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwTasks_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;
                DataLayer.Task task = (DataLayer.Task)e.Item.DataItem;
                if (task.DueDate != null && task.DueDate <= DateTime.Now)
                {
                    HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("TaskListRow");
                    if (row != null)
                    {
                        row.Attributes.Add("class", "listviewHighlighted");
                    }
                }

             
            }

        }

        protected void lnkExport_Command(object sender, CommandEventArgs e)
        {
            TaskBLL taskBLL = new TaskBLL();
            //Create the searchDTO according to the current field entries
            TaskSearchDTO searchDTO = new TaskSearchDTO();
            searchDTO.DateFrom = string.IsNullOrWhiteSpace(this.txtDateFrom.Text)?new DateTime(1900,01,01):Convert.ToDateTime(this.txtDateFrom.Text);
            searchDTO.DateTo = string.IsNullOrWhiteSpace(this.txtDateTo.Text) ? new DateTime(2100, 12, 31) : Convert.ToDateTime(this.txtDateTo.Text);
            searchDTO.TaskCategoryId = string.IsNullOrWhiteSpace(this.ddlTaskCategory.Text) ?0:Convert.ToInt32(this.ddlTaskCategory.Text);
            searchDTO.TaskClosingType = this.ddlClosingType.Text;
            searchDTO.TaskPriority = this.ddlPriority.Text;
            searchDTO.TaskStatus = this.ddlTaskStatus.Text;
            searchDTO.Text = this.txtFreeText.Text;

            IList<TaskToCsvDTO> csvObjs = taskBLL.GetFilteredTasksForCsv(searchDTO);
            string csvlist = CSVBuilder<TaskToCsvDTO>.ToCsv(";", csvObjs);
            SendFileToClient(csvlist);
            
        }

        private void SendFileToClient(string data)
        {
            
                         
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();
            //Add the header & other information      
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.CacheControl = "private";
            Response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
            Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
            Response.AppendHeader("Content-Length", data.Length.ToString());
            Response.AppendHeader("Pragma", "cache");
            Response.AppendHeader("Expires", "60");
            Response.AppendHeader("Content-Disposition",
            "attachment; " +
            "filename=\"ExcelReport.xlsx\"; " +
            "size=" + data.Length.ToString() + "; " +
            "creation-date=" + DateTime.Now.ToString("R") + "; " +
            "modification-date=" + DateTime.Now.ToString("R") + "; " +
            "read-date=" + DateTime.Now.ToString("R"));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Write it back to the client    
            Response.WriteFile(data);
            Response.End();
        }

    }
}