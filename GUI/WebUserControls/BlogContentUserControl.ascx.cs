using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer;
using EbalitWebForms.DataLayer;
using System.Web.Security;

namespace EbalitWebForms.GUI.WebUserControls
{
    public partial class BlogContentUserControl : System.Web.UI.UserControl
    {
        public bool IsPublishedDateVisible { get; set; }
        
        public bool IsCommentsVisible { get; set; }
        
        public string BlogTopic { get; set; }

        public string CurrentEntryID { 
            get { return ViewState["CurrentEntryID"].ToString() ;}
            set { ViewState.Add("CurrentEntryID", value);
            dvwEntry.DataBind();
            dtlComments.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        
            MembershipUser user = Membership.GetUser();
            if (user != null && user.UserName.ToLower() == "administrator" && user.IsOnline)
            {
                this.btnEdit.Visible = true;
            }
            else
            {
                this.btnEdit.Visible = false;
            }
            
            this.lblShowPopup.Visible = IsCommentsVisible;
            this.ModalPopupExtender1.Enabled = IsCommentsVisible;
            this.Popup.Visible = IsCommentsVisible;

            this.dvwEntry.Fields[1].Visible = IsPublishedDateVisible;

            BlogEntryDAL blogEntryDAL = new BlogEntryDAL();
            BlogEntry blogEntry;
            if (!IsPostBack)
            {
                int BlogEntryId = Convert.ToInt32(Request.Params["BlogEntryID"]);

                //if there is a request param, take it, otherwise get the default entry
                if (BlogEntryId != 0)
                {
                    blogEntry = blogEntryDAL.GetBlogEntry(BlogEntryId);
                }
                else
                {
                    blogEntry = blogEntryDAL.GetDefaultBlogEntry(BlogTopic);
                }

                //save the id of the blogEntry (if it exists) to the view state, in order that it is selected in the ods selecting event
                if (blogEntry != null)
                {
                    CurrentEntryID = blogEntry.Id.ToString();
                }
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
            if (ViewState["CurrentEntryID"] == null)
            {
                int BlogTopicId = new BlogTopicDAL().GetBlogTopicId(BlogTopic);
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

        protected void dvwBlogComment_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            e.Values["FK_BlogEntry"] = CurrentEntryID;
            e.Values["PostedOn"] = DateTime.Now;
        }

        protected void odsBlogComments_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["blogEntryId"] = CurrentEntryID;
        }


        protected void lnkSend_Command(object sender, CommandEventArgs e)
        {
            if (Convert.ToInt32(CurrentEntryID) > 0)
            {
                dvwBlogComment.InsertItem(true);
                //refresh the Comments datalist and set the comment details view into edit state again
                dtlComments.DataBind();
            }
            this.dvwBlogComment.ChangeMode(DetailsViewMode.Insert);

        }

        /// <summary>
        /// Editing is done on the details screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            BlogTopicDAL blogTopicBLL = new BlogTopicDAL();
            int blogTopicID = blogTopicBLL.GetBlogTopicId(BlogTopic);
            Response.Redirect(string.Format("/GUI/ProtectedSites/CreateBlogEntry.aspx?Id={0}&BlogTopicID={1}", CurrentEntryID, blogTopicID));
        }


        /// <summary>
        /// Send the inserted comment to the predefined mail recepient using the singleton mailmanager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsBlogComment_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BlogComment insertedComment = (BlogComment)e.ReturnValue;
            if (insertedComment != null)
            {
                MailManager.GetMailManager().SendCommentMessage(insertedComment);
            }
        }


    }
}