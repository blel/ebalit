using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WineDatabase
{
    public partial class ManageWines : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDetails_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("/GUI/WineDatabase/CreateWine.aspx?Id={0}",e.CommandArgument));
        }

        protected void odsWines_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                this.lblStatus.Text = "Deletion not possible. Make sure there are no depending objects and try again.";
                e.ExceptionHandled = true;
            }
        }


    }
}