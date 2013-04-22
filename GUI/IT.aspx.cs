using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EbalitWebForms.DataLayer;
using EbalitWebForms.BusinessLogicLayer;

namespace EbalitWebForms.GUI
{
    public partial class WebForm3 : System.Web.UI.Page
    {


        protected void Page_Init(object sender, EventArgs e)
        {
            CategoryAccordionHelper categoryAccordionHelper = new CategoryAccordionHelper(Accordion);
            categoryAccordionHelper.CreateAccordion("IT", linkButton_Command);

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
                hdfSelectedPane.Value = Convert.ToString(blogCategoryDAL.GetCategoryAccordionIndex(blogEntry.Category));
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