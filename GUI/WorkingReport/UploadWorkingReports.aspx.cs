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

                    lvwErroneousRecords.DataSource = erroneousRecords;
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
    }
}