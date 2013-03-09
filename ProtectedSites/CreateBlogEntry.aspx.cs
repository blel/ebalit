using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace EbalitWebForms.ProtectedSites
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page is loaded for the first time...
            if (!IsPostBack)
            {

                int Id = Convert.ToInt32( Request.QueryString["Id"]);
                //if there is a valid id in the parameter string...
                if (Id != 0)
                {
                    //add id to the view state and display the record in edit view
                    ViewState.Add("Id", Id);
                    this.DetailsView1.ChangeMode(DetailsViewMode.Edit);
                }
                else
                //otherwise...
                {
                    //go into insert mode
                    this.DetailsView1.ChangeMode(DetailsViewMode.Insert);
                }
            }
            //if it is a postback, accept the standard behaviour of the details view control (just display the record)
        }

        /// <summary>
        /// To accept dates in other formats than us.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Takes the id from the view state and displays the record in the details view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["Id"] = Convert.ToInt32(ViewState["Id"]);         
        }

        /// <summary>
        /// Adds the inserted id to the view state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ViewState.Add("Id", e.ReturnValue.ToString());
        }

        protected void DetailsView1_ItemInserting1(object sender, DetailsViewInsertEventArgs e)
        {
            e.Values["PublishedOn"] = GetUSDate(e.Values["PublishedOn"].ToString());
        }

        private string GetUSDate(string anyDate)
        {
            DateTime currentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(anyDate) && DateTime.TryParse(anyDate, out currentDate))
            {
                return currentDate.ToString(new CultureInfo("en-US"));
            }
            return string.Empty;
        }

        protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            e.NewValues["PublishedOn"] = GetUSDate(e.NewValues["PublishedOn"].ToString());
        }



        protected void ddlTopic_TextChanged(object sender, EventArgs e)
        {
            PopulateDdlCategory(Convert.ToInt32(((DropDownList)sender).SelectedValue));
        }


        private void PopulateDdlCategory(int BlogTopicID)
        {
            DropDownList ddlCategory = (DropDownList)DetailsView1.FindControl("ddlCategory");
            ddlCategory.Items.Clear();
            BlogCategoryDAL categoryBLL = new BlogCategoryDAL();
            foreach (var item in categoryBLL.ReadBlogCategory(BlogTopicID))
            {
                ddlCategory.Items.Add(new ListItem(item.Category, item.Id.ToString()));
            }
        }

        protected void DetailsView1_DataBinding(object sender, EventArgs e)
        {

        }

        protected void ddlCategory_DataBinding(object sender, EventArgs e)
        {
            //BlogTopic
            var BlogTopicID = Request.QueryString["BlogTopicId"];
            if (BlogTopicID == null)
            {
                PopulateDdlCategory(new BlogTopicDAL().ReadBlogTopic().First().Id); 
            }
            else
            {


                PopulateDdlCategory(Convert.ToInt32(BlogTopicID));
            }
        }




    }
}