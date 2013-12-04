using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSVParser;
using EbalitWebForms.BusinessLogicLayer.CsvFileImport;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;
using EbalitWebForms.GUI.WebUserControls;

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
            StatusBar.StatusText = string.Format("File could not be imported: Error at line {0}, col {1}: {2}", line,
                col, message);
        }

        /// <summary>
        /// Preselect values of ddls if possible 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvwErroneousRecords_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //TODO: to be continued...
            if (lvwErroneousRecords.EditItem!=null && e.Item.DataItemIndex == lvwErroneousRecords.EditItem.DataItemIndex)
            {
                var ddlProjects = (DropDownList) GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlProjects");
                var erroneousEntity = (DataLayer.ErroneousWorkingReport) e.Item.DataItem;
                if (ddlProjects.Items.FindByText(erroneousEntity.ProjectName) != null)
                {
                    ddlProjects.SelectedValue = erroneousEntity.ProjectName;

                    var projectBll = new ProjectBll();
                    var projectEntity = projectBll.GetItems().SingleOrDefault(cc => cc.Name == ddlProjects.SelectedValue);
                    if (projectEntity != null)
                    {
                        odsResources.SelectParameters["projectId"].DefaultValue = projectEntity.Id.ToString();
                        var ddlResources =
                            (DropDownList) GUIHelper.RecursiveFindControl(lvwErroneousRecords, "ddlResources");
                        ddlProjects.DataBind();
                    }
                    else
                    {
                        odsResources.SelectParameters["projectId"].DefaultValue = string.Empty;
                    }

                }
            }
        }
    }
}