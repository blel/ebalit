using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.Controlling
{
    public partial class ManageProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkProjectStructure_Click(object sender, EventArgs e)
        {
            Response.Redirect("WorkBreakdownStructure.aspx");
        }
    }
}