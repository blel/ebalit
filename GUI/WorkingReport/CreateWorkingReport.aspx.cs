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
            var taskTextBox = (TextBox)GUIHelper.RecursiveFindControl(this,"txtTask");
            if (taskTextBox != null)
            {
                taskTextBox.Text = trvTask.SelectedValue;
            }
        }
    }
}