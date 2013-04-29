using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace EbalitWebForms.GUI.ProtectedSites
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkLogout_Command(object sender, CommandEventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/GUI/Admin.aspx");
        }
    }
}