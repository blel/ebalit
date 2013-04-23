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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }

        }

        /// <summary>
        /// Checks whether there is a CurrentEntryID in the view state.
        /// If not, load default blog entry, otherwise load entry with the ID = CurrentEntryID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsBlogEntry_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (ViewState["CurrentEntryID"] == null)
            {
                //int BlogTopicId = new BlogTopicDAL().GetBlogTopicId("Home");
                //BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
                //int Id = blogEntryDAL.GetDefaultBlogEntryId(BlogTopicId);
                //e.InputParameters["Id"] = Id;

                BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
                BlogEntry blogEntry = blogEntryDAL.GetDefaultBlogEntry("Home");
                e.InputParameters["Id"] = blogEntry.Id;
                ViewState.Add("CurrentEntryID", blogEntry.Id);
            }
            else
            {
                e.InputParameters["Id"] = ViewState["CurrentEntryID"];
            }
        }

        protected void lnkButton_Command(object sender, CommandEventArgs e)
        {
            BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
            int Id = Convert.ToInt32(e.CommandArgument);
            BlogEntry blogEntry = blogEntryDAL.GetBlogEntry(Id);
            switch (blogEntry.BlogCategory.BlogTopic.Topic)
            {
                case "Music":
                    Response.Redirect(string.Format("/GUI/Music.aspx?BlogEntryID={0}", blogEntry.Id));
                    break;
                case "IT":
                    Response.Redirect(string.Format("/GUI/IT.aspx?BlogEntryID={0}", blogEntry.Id));
                    break;
                case "Home":
                    Response.Redirect(string.Format("/GUI/Index.aspx?BlogEntryID={0}", blogEntry.Id));
                    break;
                case "Varia":
                    Response.Redirect(string.Format("/GUI/Varia.aspx?BlogEntryID={0}", blogEntry.Id));
                    break;

                default:

                    break;
            }


        }
    }
}