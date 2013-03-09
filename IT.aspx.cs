using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace EbalitWebForms
{
    public partial class WebForm3 : System.Web.UI.Page
    {


        protected void Page_Init(object sender, EventArgs e)
        {
            BlogCategoryDAL categoryDAL = new BlogCategoryDAL();
            BlogTopicDAL topicDAL = new BlogTopicDAL();
            BlogEntryDAL entryDAL = new BlogEntryDAL();
            Accordion.Panes.Clear();
            foreach (BlogCategory category in categoryDAL.ReadBlogCategory(topicDAL.GetBlogTopicId("IT")))
            {
                AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane();
                pane.ID = "pane" + category.Id;
                pane.HeaderContainer.Controls.Add(new LiteralControl(category.Category));
                foreach (BlogEntry entry in entryDAL.GetBlogEntries(category.Id))
                {
                    LinkButton linkButton = new LinkButton();
                    linkButton.ID = "linkButton" + entry.Id;
                    linkButton.Text = entry.Subject;
                    linkButton.CommandArgument = entry.Id.ToString();
                    linkButton.Command += new CommandEventHandler(linkButton_Command);
                    pane.ContentContainer.Controls.Add(linkButton);
                    pane.ContentContainer.Controls.Add(new LiteralControl("<br>"));
                }
                Accordion.Panes.Add(pane);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(!string.IsNullOrEmpty (hdfSelectedPane.Value))
                    Accordion.SelectedIndex = Convert.ToInt32(hdfSelectedPane.Value);
            }


        }

        protected void odsBlogEntry_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            
            if (ViewState["CurrentEntryID"] ==null)
            {
                int BlogTopicId = new BlogTopicDAL().GetBlogTopicId("IT");
                e.InputParameters["Id"] = new BlogEntryDAL().GetDefaultBlogEntryId(BlogTopicId);
            }
            else
            {
                e.InputParameters["Id"] = ViewState["CurrentEntryID"];
            }
            
        }

        protected void linkButton_Command(object sender, CommandEventArgs e)
        {
            ViewState.Add("CurrentEntryID",e.CommandArgument);
            dvwEntry.DataBind();
            
          
        }

        protected void odsBlogEntry_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }




    }
}