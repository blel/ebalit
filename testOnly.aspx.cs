using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms
{
    public partial class testOnly : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string test = TextBox1.Text;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string test = TextBox1.Text;
        }
    }
}