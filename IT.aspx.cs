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

            //setup the accordion
            Accordion.Panes.Clear();
            foreach (BlogCategory category in categoryDAL.ReadBlogCategory(topicDAL.GetBlogTopicId("IT")))
            {
                AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane();
                pane.ID = "pane" + category.Id;

                LiteralControl header = new LiteralControl(string.Format("<div class = \"MenuHeader\"><b>{0}</b></div>", category.Category));
                
                pane.HeaderContainer.Controls.Add(header);
                foreach (BlogEntry entry in entryDAL.GetBlogEntries(category.Id))
                {
                    LinkButton linkButton = new LinkButton();
                    linkButton.ID = "linkButton" + entry.Id;
                    linkButton.Text = entry.Subject;  
                    linkButton.CommandArgument = entry.Id.ToString();
                    linkButton.Command += new CommandEventHandler(linkButton_Command);
                    linkButton.CausesValidation = false;
                    linkButton.CssClass = "MenuButton";
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
                if (!string.IsNullOrEmpty(hdfSelectedPane.Value))
                    Accordion.SelectedIndex = Convert.ToInt32(hdfSelectedPane.Value);
            }
            else
            {
                int BlogTopicId = new BlogTopicDAL().GetBlogTopicId("IT");
                BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
                int BlogEntryId = blogEntryDAL.GetDefaultBlogEntryId(BlogTopicId);
                BlogEntry blogEntry = blogEntryDAL.GetBlogEntry(BlogEntryId);
                BlogCategoryDAL blogCategoryDAL = new BlogCategoryDAL();

                Accordion.SelectedIndex = blogCategoryDAL.GetCategoryAccordionIndex(blogEntry.Category);
            }

            this.dvwBlogComment.ChangeMode(DetailsViewMode.Insert);

        }

        /// <summary>
        /// Checks whether there is a CurrentEntryID in the view state.
        /// If not, load default blog entry, otherwise load entry with the ID = CurrentEntryID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsBlogEntry_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (ViewState["CurrentEntryID"] ==null)
            {
                int BlogTopicId = new BlogTopicDAL().GetBlogTopicId("IT");
                BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
                int Id = blogEntryDAL.GetDefaultBlogEntryId(BlogTopicId);
                e.InputParameters["Id"] = Id;
                

                ViewState.Add("CurrentEntryID", e.InputParameters["Id"]);
            }
            else
            {
                e.InputParameters["Id"] = ViewState["CurrentEntryID"];
            }
        }

        /// <summary>
        /// LinkButtons in the accordion. When pressed, the Id of the selected entry is saved in the view state
        /// and databinding is executed --> see odsBlogEntry_Selecting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkButton_Command(object sender, CommandEventArgs e)
        {
            ViewState.Add("CurrentEntryID",e.CommandArgument);
            dvwEntry.DataBind();
            dtlComments.DataBind();
        }

        protected void lnkSend_Command(object sender, CommandEventArgs e)
        {
            
            dvwBlogComment.InsertItem(true);
            //refresh the Comments datalist and set the comment details view into edit state again
            dtlComments.DataBind();
            this.dvwBlogComment.ChangeMode(DetailsViewMode.Insert);

        }

        protected void dvwBlogComment_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            e.Values["FK_BlogEntry"] = ViewState["CurrentEntryID"];
            e.Values["PostedOn"] = DateTime.Now;
        }

        protected void odsBlogComments_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["blogEntryId"] = ViewState["CurrentEntryID"];
        }

 
    }
}