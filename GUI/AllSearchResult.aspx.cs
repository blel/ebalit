using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //show the search text in the search textfield
                this.SearchUserControl.SearchString = Request.Params["searchText"];
            }
        }

        protected void lnkSearchResult_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("/GUI/{0}", e.CommandArgument.ToString() == string.Empty ? string.Empty :e.CommandArgument));
        }

        protected void SearchUserControl_SearchButtonClick(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("/GUI/AllSearchResult.aspx?searchText={0}", this.SearchUserControl.SearchString));
        }
    }
}