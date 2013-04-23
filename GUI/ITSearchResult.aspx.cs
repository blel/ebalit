using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //show the search text in the search textfield
                this.txtSearch.Text = Request.Params["searchText"];
            }
        }
        protected void lnkSearchResult_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("/GUI/IT.aspx{0}", e.CommandArgument.ToString() == string.Empty ? string.Empty : "?BlogEntryID=" + e.CommandArgument));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("/GUI/ITSearchResult.aspx?blogTopic={0}&searchText={1}", "IT", this.txtSearch.Text));
        }
    }
}