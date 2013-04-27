using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //insert the search text in the search field
                this.SearchUserControl.SearchString = Request.Params["searchText"];
            }
        }
        protected void lnkSearchResult_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("/GUI/Varia.aspx{0}", e.CommandArgument.ToString() == string.Empty ? string.Empty : "?BlogEntryID=" + e.CommandArgument));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("/GUI/VariaSearchResult.aspx?blogTopic={0}&searchText={1}", "Varia", this.SearchUserControl.SearchString));
        }
    }
}