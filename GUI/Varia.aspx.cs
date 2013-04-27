using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.GUI
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
  
        }
        /// <summary>
        /// LinkButtons in the accordion. When pressed, the Id of the selected entry is saved in the view state
        /// and databinding is executed --> see odsBlogEntry_Selecting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkButton_Command(object sender, CommandEventArgs e)
        {
            this.BlogContentUserControl.CurrentEntryID = e.CommandArgument.ToString(); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("/GUI/VariaSearchResult.aspx?blogTopic={0}&searchText={1}", "Varia", this.SearchUserControl.SearchString));
        }
    }
}