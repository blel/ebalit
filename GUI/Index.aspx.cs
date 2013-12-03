using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.DataLayer;
using EbalitWebForms.BusinessLogicLayer;

namespace EbalitWebForms.GUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

        }


        protected void lnkButton_Command(object sender, CommandEventArgs e)
        {
            BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
            int Id = Convert.ToInt32(e.CommandArgument);
            BlogEntry blogEntry = blogEntryDAL.GetBlogEntry(Id);
            Response.Redirect(string.Format("/GUI/{0}?BlogEntryID={1}", blogEntry.BlogCategory.BlogTopic.PageName, blogEntry.Id));
        }

        /// <summary>
        /// Occurs when user presses the links in the archive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Archive_LinkButtonPressed(object sender, CommandEventArgs e)
        {
            int Id = Convert.ToInt32(e.CommandArgument);
            BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
            BlogEntry blogEntry = blogEntryDAL.GetBlogEntry(Id);
            if (blogEntry != null)
            {
                string pageName = blogEntry.BlogCategory.BlogTopic.PageName;
                Response.Redirect(string.Format("/GUI/{0}?BlogEntryID={1}", pageName, Id));
            }
        }

        protected void CategoryBrowser_LinkButtonPressed(object sender, CommandEventArgs e)
        {

        }

        protected void SearchUserControl_SearchButtonClick(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("/GUI/AllSearchResult.aspx?searchText={0}",  this.SearchUserControl.SearchString));
        }
    }
}