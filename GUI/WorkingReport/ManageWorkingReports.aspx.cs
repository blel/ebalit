using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer.DTO;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class ManageWorkingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: This seems to work, but need to checkout why exactly...
            scmAjaxToolkit.RegisterPostBackControl(trvTask);
        }

        /// <summary>
        /// When resources are selected from the db,
        /// it is checked whether there is a particular project selected, for which the resources 
        /// should be displayed. If none is selected, nothing will be shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsResources_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ddlProjects.SelectedValue))
            {
                e.InputParameters["projectId"] = Convert.ToInt32(ddlProjects.SelectedValue);
            }
        }


        /// <summary>
        /// Assign selected text to the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trvTask_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            txtTaskDropDown.Text = trvTask.SelectedNode.Text;

            ViewState.Add("taskGuid", trvTask.SelectedNode.DataPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCreate_Command(object sender, CommandEventArgs e)
        {
            
            Response.Redirect("/GUI/WorkingReport/CreateWorkingReport.aspx");
        }

        /// <summary>
        /// Check whether the working report is updateable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDetails_OnCommand(object sender, CommandEventArgs e)
        {
            var workingReportBll = new WorkingReportBll();
            if (workingReportBll.IsWorkingReportUpdateable(Convert.ToInt32(e.CommandArgument)))
            {
                Response.Redirect(string.Format("/GUI/WorkingReport/CreateWorkingReport.aspx?Id={0}", e.CommandArgument));
            }
        }

        /// <summary>
        /// Returns the difference between to and from for the working report
        /// with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetTimeSpan(object id)
        {
            var workingReportBll = new WorkingReportBll();
            var currentRecord = workingReportBll.GetWorkingReport(Convert.ToInt32(id));
            if (currentRecord != null)
            {
                return (currentRecord.To.GetValueOrDefault() - 
                    currentRecord.From.GetValueOrDefault()).ToString(@"hh\:mm", new CultureInfo("de-CH").DateTimeFormat);
            }
            return string.Empty;
        }

        /// <summary>
        /// Update resuorce and task filters when project changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProjects_OnTextChanged(object sender, EventArgs e)
        {
            ddlResource.DataBind();
            trvTask.DataBind();
        }

        /// <summary>
        /// Make sure only tasks from the selected project are shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void htsTasks_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["ProjectId"] = ddlProjects.SelectedValue;
        }


        /// <summary>
        /// When data of the ddl Project is bound, make sure depending lists are loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProjects_OnDataBound(object sender, EventArgs e)
        {
            trvTask.DataBind();
            ddlResource.DataBind();
        }

        /// <summary>
        /// Find method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkFind_OnCommand(object sender, CommandEventArgs e)
        {
            DateTime? fromTime = null;
            
            DateTime? toTime = null;

            try
            {
                fromTime = DateTime.Parse(txtDateFrom.Text, CultureInfo.CurrentCulture.DateTimeFormat,
                    DateTimeStyles.None);
            }
            catch (FormatException)
            {
                //todo: validation
            }
            catch (ArgumentNullException)
            {
                //todo: validation
            }
            catch (ArgumentException)
            {
                //todo: validation
            }

            try
            {
            toTime = DateTime.Parse(txtDateTo.Text, CultureInfo.CurrentCulture.DateTimeFormat,
                    DateTimeStyles.None);
            }
            catch (FormatException)
            {
                //todo: validation
            }
            catch (ArgumentNullException)
            {
                //todo: validation
            }
            catch (ArgumentException)
            {
                //todo: validation
            }

            int? projectId = null;
            if (!string.IsNullOrWhiteSpace(ddlProjects.SelectedValue))
            {
                projectId = Convert.ToInt32(ddlProjects.SelectedValue);
            }

            int? resourceId = null;
            if (!string.IsNullOrWhiteSpace(ddlResource.SelectedValue))
            {
                resourceId = Convert.ToInt32(ddlResource.SelectedValue);
            }

            var findDto = new WorkingReportFindDto
           {
               From = fromTime,
               To = toTime,
               ProjectId = projectId,
               ResourceId = resourceId,
               TaskGuid = Convert.ToString(ViewState["taskGuid"])
           } ;

            ViewState.Add("findDto", findDto);

            lvwWorkingReports.DataBind();
        }

        protected void odsWorkingReports_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["findDto"] = ViewState["findDto"];

        }

        protected void lnkClear_OnCommand(object sender, CommandEventArgs e)
        {
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            txtTaskDropDown.Text = string.Empty;
            ddlProjects.SelectedIndex = 0;
            ddlResource.SelectedIndex = 0;


        }

        protected void vdtDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.Value))
            {
                var date = new DateTime();

                try
                {
                    args.IsValid = DateTime.TryParse(args.Value, CultureInfo.CurrentCulture.DateTimeFormat,
                        DateTimeStyles.None, out
                            date);
                }
                catch (ArgumentException)
                {
                    args.IsValid = false;
                }
                catch (NotSupportedException)
                {
                    args.IsValid = false;
                }
            }
        }
    }
}