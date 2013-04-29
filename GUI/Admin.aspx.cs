using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.DataLayer;
using System.Web.Security;

namespace EbalitWebForms.GUI
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ctlLogin.Focus();
        }

        protected void ctlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = Membership.ValidateUser(this.ctlLogin.UserName, this.ctlLogin.Password);
        }

    }
}