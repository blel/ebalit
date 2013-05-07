﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.Security;

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
            if (e.NewValues["DueDate"]!=null)
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
            var mpe = RecursiveFindControl(this.lvwTasks, "ModalPopupExtender1");
            if (mpe!= null)

                ((AjaxControlToolkit.ModalPopupExtender)mpe).Show();
        }

        /// <summary>
        /// Searches for a control recursively
        /// </summary>
        /// <param name="control"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwTaskComments_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            e.Values["FK_Task"] = hdfSelectedTaskId.Value;
            e.Values["CreatedOn"] = DateTime.Now;
            e.Values["CreatedBy"] = Membership.GetUser()!=null?Membership.GetUser().ToString():"anonymous";
        }

        protected void lvwTaskComments_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            e.NewValues["FK_Task"] = hdfSelectedTaskId.Value;
            e.NewValues["ChangedOn"] = DateTime.Now;
            e.NewValues["ChangedBy"] = Membership.GetUser()!= null ? Membership.GetUser().ToString() : "anonymous";
        }
  }
}