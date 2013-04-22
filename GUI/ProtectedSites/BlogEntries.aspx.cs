using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.DataLayer;
using EbalitWebForms.BusinessLogicLayer;

namespace EbalitWebForms.GUI.ProtectedSites
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }

        protected void odsBlogEntries_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int BlogEntryId = Convert.ToInt32(((GridView)sender).DataKeys[e.NewSelectedIndex].Value);
            BlogEntry entry = new BlogEntryDAL().GetBlogEntry(BlogEntryId);
            BlogCategory category = entry.BlogCategory;
            int BlogTopicId = category.FK_Topic;

            Response.Redirect(string.Format("CreateBlogEntry.aspx?Id={0}&BlogTopicID={1}", BlogEntryId, BlogTopicId));
            ;
        }

        protected void odsBlogEntries_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            
        }


    }
}