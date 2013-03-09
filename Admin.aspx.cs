using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ctlLogin.Focus();
        }

        protected void ctlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Ebalit_WebFormsEntities entities = new Ebalit_WebFormsEntities();
            EbalitWebForms.User user = (from cc in entities.Users
                                        where cc.Username == this.ctlLogin.UserName &&
                                              cc.Password == this.ctlLogin.Password
                                        select cc).FirstOrDefault();
            if (user != null)
                e.Authenticated = true;
            else
                e.Authenticated = false;

        }
    }
}