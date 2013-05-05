using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace EbalitWebForms.GUI.TaskManager
{
    public partial class TaskList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
    
            }
        }

        protected void ddlTaskCategory_DataBound(object sender, EventArgs e)
        {
            ddlTaskCategory.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            ddlTaskCategory.SelectedIndex = 0;
        }

        protected void lnkFind_Command(object sender, CommandEventArgs e)
        {


            this.lvwTasks.DataBind();
            
        }

        protected void odsTasks_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filter"]=GetTaskSearchDTO();
        }

        private BusinessLogicLayer.TaskSearchDTO GetTaskSearchDTO()
        {
            BusinessLogicLayer.TaskSearchDTO taskSearchDTO = new BusinessLogicLayer.TaskSearchDTO();
            taskSearchDTO.DateFrom = string.IsNullOrWhiteSpace(txtDateFrom.Text) ? new DateTime(1900,1,1) : Convert.ToDateTime(this.txtDateFrom.Text);
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

        protected void lvwTasks_DataBound(object sender, EventArgs e)
        {
            if (this.lvwTasks.EditItem != null)
            {
                DropDownList taskCategory = (DropDownList)this.lvwTasks.EditItem.FindControl("ddlTaskCategory");
                if (taskCategory != null)
                {
                    taskCategory.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    int TaskId = lvwTasks.EditItem.DataItemIndex;
                    BusinessLogicLayer.TaskBLL taskBLL = new BusinessLogicLayer.TaskBLL();
                    
                    DataLayer.Task task = taskBLL.GetTaskById(TaskId);
                    if (task.FK_TaskCategory != null && task.FK_TaskCategory != 0)
                        taskCategory.SelectedValue = Convert.ToString(task.FK_TaskCategory);
                }
            }

        }

        protected void lvwTasks_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            //to cope with unbound ddlTaskCategory 
            DropDownList ddlTaskCategory = (DropDownList)this.lvwTasks.EditItem.FindControl("ddlTaskCategory");
            if (ddlTaskCategory != null)
            {
                if (!string.IsNullOrWhiteSpace(ddlTaskCategory.SelectedValue))
                {
                    e.NewValues["FK_TaskCategory"] = ddlTaskCategory.SelectedValue;
                }
            }
            //to workaround us date bug
            e.NewValues["DueDate"] = GUIHelper.GetUSDate(e.NewValues["DueDate"].ToString());
        }



        protected void btnDetails_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("~/GUI/TaskManager/TaskDetail.aspx?Id={0}", e.CommandArgument));
        }
        public void test()
        {
            this.lvwTaskComments.DataBind();
        }

        protected void btnComments_Command(object sender, CommandEventArgs e)
        {
            this.hdfSelectedTaskId.Value = e.CommandArgument.ToString();
            this.lvwTaskComments.DataBind();


            var mpe = RecursiveFindControl(this.lvwTasks, "ModalPopupExtender1");
            if (mpe!= null)

                ((AjaxControlToolkit.ModalPopupExtender)mpe).Show();
            
        }

        private Control RecursiveFindControl(Control control, string controlName)
        {
            Debug.WriteLine(control.ID);
            if (control.ID == controlName)
                return control;

            foreach (Control childControl in control.Controls)
            {
                Control t = RecursiveFindControl(childControl, controlName);
                if (t != null) return t;
            }
            return null;
        }

        protected void odsTaskComments_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            
        }

        protected void lvwTaskComments_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            e.Values["FK_Task"] = hdfSelectedTaskId.Value;
        }


    
  }
}