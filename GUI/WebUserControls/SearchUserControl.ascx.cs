using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WebUserControls
{
    public partial class SearchUserControl : System.Web.UI.UserControl
    {
        public event EventHandler SearchButtonClick;
        public string SearchString { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SearchString))
                this.txtSearch.Text = SearchString;
        }




        protected void btnSearch_Command(object sender, CommandEventArgs e)
        {
            SearchString = this.txtSearch.Text;
            if (SearchButtonClick != null)
                SearchButtonClick(sender, e);
        }
    }
}