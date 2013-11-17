using System;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace EbalitWebForms.GUI
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the focus accordingly
            ctlLogin.Focus();
        }

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ctlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //Use membership to authenticate the user
            e.Authenticated = Membership.ValidateUser(ctlLogin.UserName, ctlLogin.Password);
        }

    }
}