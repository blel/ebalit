using System;
using System.Globalization;
using System.Web.UI.WebControls;
using CsvParser;
using CSVParser;
using EbalitWebForms.BusinessLogicLayer;
using EbalitWebForms.BusinessLogicLayer.CsvFileImport;
using EbalitWebForms.BusinessLogicLayer.DTO;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;
using EbalitWebForms.Common;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class ManageWorkingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: This seems to work, but need to checkout why exactly...
            scmAjaxToolkit.RegisterPostBackControl(trvTask);



            if (!IsPostBack)
            {

                //retrieve filter parameters from session and update fields with
                //appropriate values
                var findDto = (WorkingReportFindDto)Session["findDto"];
                if (findDto != null)
                {
                    txtDateFrom.Text = string.Format("{0:d}", findDto.From);

                    txtDateTo.Text = string.Format("{0:d}", findDto.To);

                    ddlProjects.SelectedValue = findDto.ProjectId.ToString();

                    ddlResource.SelectedValue = findDto.ResourceId.ToString();

                    ViewState.Add("taskGuid", findDto.TaskGuid);

                    txtTaskDropDown.Text = new WorkingReportBll().GetTaskPath(findDto.TaskGuid);
                }
            }
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
        /// Go to create page when create button is clicked
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
           };

            Session.Add("findDto", findDto);

            lvwWorkingReports.DataBind();
        }

        /// <summary>
        /// Make sure filter is applied when selecting working reports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsWorkingReports_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["findDto"] = Session["findDto"];

        }

        /// <summary>
        /// Clear all filter parameters and any stored view state or session parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkClear_OnCommand(object sender, CommandEventArgs e)
        {
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            txtTaskDropDown.Text = string.Empty;
            ddlProjects.SelectedIndex = 0;
            ddlResource.SelectedIndex = 0;
            Session.Remove("findDto");
            ViewState.Remove("taskGuid");


        }

        /// <summary>
        /// Check whether value is a valid datae
        /// TODO: convert to custom control
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
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

        /// <summary>
        /// Export data to csv file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkExport_OnCommand(object sender, CommandEventArgs e)
        {
            var workingReportBll = new WorkingReportBll();
            var dataList = workingReportBll.
                GetWorkingReports((WorkingReportFindDto)Session["findDto"]).
                ForEach(cc => cc.ToCsvDto());

            var data = CSVBuilder.ToCsv(";", dataList);

            FileDownloader.Data = data;
            FileDownloader.SendFileToClient();
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

                    var result = parser.Read();
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