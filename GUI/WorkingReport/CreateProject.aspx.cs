using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer.WorkingReport;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class CreateProject : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if site is really loaded and no postback
            if (!IsPostBack)
            {
                //if there is a "Id" parameter in the url change to edit mode
                if (Request.QueryString["Id"] != null)
                {
                    dtvProject.ChangeMode(DetailsViewMode.Edit);
                }
                //otherwise go to insert mode
                else
                {
                    dtvProject.ChangeMode(DetailsViewMode.Insert);
                }
            }
        }

        /// <summary>
        /// Create a Guid for this project before inserting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsProjects_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var projectEntity = (ProjectProject) e.InputParameters["item"];

            if (projectEntity != null)
            {
                projectEntity.Guid = Guid.NewGuid();
            }
        }

        /// <summary>
        /// pass id to select method if available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsProjects_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["Id"] = Request.QueryString["Id"];
        }

        /// <summary>
        /// Redirect after insert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsProjects_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Response.Redirect("/GUI/WorkingReport/ManageProjects.aspx");
        }

        /// <summary>
        /// Redirect after update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsProjects_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Response.Redirect("/GUI/WorkingReport/ManageProjects.aspx");
        }

        /// <summary>
        /// Redirect after delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvProject_OnModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit)
            {
                e.Cancel = true;
                Response.Redirect("/GUI/WorkingReport/ManageProjects.aspx");
            }
        }

        protected void CustomValidator1_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            
            var txtProject = (TextBox) GUIHelper.RecursiveFindControl(dtvProject, "txtName");

            var id = dtvProject.DataKey.Value ?? 0;

            args.IsValid = new ProjectBll().IsUnique(txtProject.Text, Convert.ToInt32(id));
        }
    }
}