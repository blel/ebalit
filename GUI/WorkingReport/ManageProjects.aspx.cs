using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WorkingReport
{
    public partial class ManageProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Go to details page and provide id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDetails_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("/GUI/WorkingReport/CreateProject.aspx?Id={0}", e.CommandArgument));
        }

        protected void odsProjects_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                StatusBar.StatusText = "Delete was not successful. Make sure there are no depending objects on this project and try again.";
                e.ExceptionHandled = true;
            }
        }

        protected void lnkCreate_OnCommand(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("/GUI/WorkingReport/CreateProject.aspx"));
        }
    }
}