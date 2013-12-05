using System;
using System.Linq;
using System.Web.UI.WebControls;
using CSVParser;
using EbalitWebForms.BusinessLogicLayer.CsvFileImport;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class UploadWorkingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkUpload_OnCommand(object sender, CommandEventArgs e)
        {
            if (fulCsvFileUpload.HasFile)
            {
                try
                {
                    var parser = new StreamCsvParser<WorkingReportCsvFile>(fulCsvFileUpload.FileContent, ";", false);

                    parser.ValidationErrorOccurred += parser_ValidationErrorOccurred;

                    var importResult = parser.Read();

                    var workingReportBll = new WorkingReportBll();

                    var erroneousRecords = workingReportBll.InsertManyWorkingReports(importResult);

                    var erroneousBll = new ErroneousWorkingReportBll();

                    erroneousBll.PersistErroneousWorkingReports(erroneousRecords);

                    lvwErroneousRecords.DataBind();

                    if (ViewState["notImportedLinesMessage"]!=null)
                    {
                        StatusBar.StatusText = ViewState["notImportedLinesMessage"].ToString();
                        ViewState.Remove("notImportedLinesMessage");
                    }
                    else
                    {
                        StatusBar.StatusText = "All lines succesfully imported. Check in pending items for lines which could not be allocated correctly.";
                    }

                }
                catch (Exception ex)
                {
                    StatusBar.StatusText = string.Format("File could not be imported: {0}", ex.Message);
                }
            }
        }



        /// <summary>
        /// The call back
        /// </summary>
        /// <param name="line"></param>
        /// <param name="col"></param>
        /// <param name="message"></param>
        protected void parser_ValidationErrorOccurred(int line, int col, string message)
        {
            var notImportedLinesMessage = ViewState["notImportedLinesMessage"];
            notImportedLinesMessage += string.Format("Line {0} could not be imported due to an error at col {1}: {2}</br>", line,
                col, message);
            ViewState.Add("notImportedLinesMessage", notImportedLinesMessage);
        }

        /// <summary>
        /// Preselect values of ddls if possible 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwErroneousRecords_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (lvwErroneousRecords.EditItem != null &&
                e.Item.DataItemIndex == lvwErroneousRecords.EditItem.DataItemIndex)
            {
                //the data bound item is the edit item, so update the drop down lists

                var ddlProjects = (DropDownList) GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlProjects");
                
                var ddlResources = (DropDownList)GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlResources");
                
                var erroneousEntity = (DataLayer.ErroneousWorkingReport) e.Item.DataItem;

                if (ddlProjects.Items.FindByText(erroneousEntity.ProjectName) != null)
                {
                    ddlProjects.SelectedValue = erroneousEntity.ProjectName;
                }

                var projectBll = new ProjectBll();
                var projectEntity = projectBll.GetItems().SingleOrDefault(cc => cc.Name == ddlProjects.SelectedValue);
                if (projectEntity != null)
                {
                    ViewState.Add("projectId", projectEntity.Id);

                    AdjustDdlResources();

                    if (ddlResources.Items.FindByText(erroneousEntity.ResourceName) != null)
                    {
                        ddlResources.SelectedValue = erroneousEntity.ResourceName;
                    }
                }
                else
                {
                    ViewState.Remove("projectId");
                    AdjustDdlResources();
                }
                var trvTask = (TreeView)GUIHelper.RecursiveFindControl(lvwErroneousRecords, "trvTask");
                scmAjaxToolkit.RegisterPostBackControl(trvTask);
                trvTask.DataBind();
            }
        }

        /// <summary>
        /// Get the projectId from View state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsResources_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = ViewState["projectId"];
        }

        /// <summary>
        /// Update the resource drop down list if ddlProjects changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProjects_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlProjects = (DropDownList)GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlProjects");
            var projectBll = new ProjectBll();
            var projectEntity = projectBll.GetItems().SingleOrDefault(cc => cc.Name == ddlProjects.SelectedValue);
            if (projectEntity != null)
            {
                ViewState.Add("projectId", projectEntity.Id);

                AdjustDdlResources();
               

            }
            var trvTask = (TreeView)GUIHelper.RecursiveFindControl(lvwErroneousRecords, "trvTask");
            trvTask.DataBind();
        }

        /// <summary>
        /// Make sure project and resource is added to the new values (not databound) and workaround 
        /// bug with dates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwErroneousRecords_OnItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var ddlProjects = (DropDownList)GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlProjects");
            var ddlResources = (DropDownList)GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlResources");
            e.NewValues["Date"] = GUIHelper.GetUSDate(e.NewValues["Date"].ToString());
            e.NewValues.Add("projectName", ddlProjects.SelectedValue);
            e.NewValues.Add("resourceName", ddlResources.SelectedValue);
        }

        /// <summary>
        /// Clear resource ddl, add the default item and data bind it
        /// </summary>
        private void AdjustDdlResources()
        {
            var ddlResources = (DropDownList)GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlResources");
            if (ddlResources != null)
            {
                ddlResources.Items.Clear();
                ddlResources.Items.Add(new ListItem("---Select a value---",string.Empty));
                ddlResources.DataBind();
            }
        }


        protected void htsTasks_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["ProjectId"] = ViewState["projectId"];

        }

        protected void trvTask_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            var tfsTaskIdTextBox = (TextBox)GUIHelper.RecursiveFindControl(lvwErroneousRecords,("TfsTaskIdTextBox"));
            tfsTaskIdTextBox.Text = ((TreeView)sender).SelectedNode.DataPath;
        }

        protected void lnkTransfer_OnCommand(object sender, CommandEventArgs e)
        {
            var workingReportBll = new WorkingReportBll();
            try
            {
                var resultList = workingReportBll.TransferWorkingReort(Convert.ToInt32(e.CommandArgument));
                if (resultList.Count > 0)
                {
                    StatusBar.StatusText = string.Format("Transfer of item not successful");
                }

                else
                {
                    lvwErroneousRecords.DataBind();
                    StatusBar.StatusText = "Transfer successful!";
                }

            }
            catch (InvalidOperationException ex)
            {
                
                StatusBar.StatusText = string.Format("Transfer not successful:{0}", ex.Message);
            }
        }
    }
}