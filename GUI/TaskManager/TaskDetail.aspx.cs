using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace EbalitWebForms.GUI.TaskManager
{
    /// <summary>
    /// Code behind of TaskDetail aspx.
    /// </summary>
    public partial class TaskDetail : System.Web.UI.Page
    {
        /// <summary>
        /// Executed on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if not a postback....
            if (!IsPostBack)
            {
                
                //deletes the filter if not coming from the tasklist.
                //don't like this globalish implementation
                if (Request.UrlReferrer != null  && Request.UrlReferrer.PathAndQuery != "/GUI/TaskManager/TaskList.aspx")
                    Session["FilterParams"] = null;

                //change to edit mode if there is an id in the query string          
                if (Request.QueryString["Id"] != null)
                {
                    this.dtvTask.ChangeMode(DetailsViewMode.Edit);
                }
                //otherwise, it's insert mode.
                else
                {
                    this.dtvTask.ChangeMode(DetailsViewMode.Insert);
                    //make sure the first index is chosen (not a good implementation)
                    //TODO: don't like this implementation - what about a new drop down list with first index = select xxx
                    ((DropDownList)this.dtvTask.FindControl("ddlState")).SelectedIndex = 1;
                }
                this.dtvTask.FindControl("txtSubject").Focus();                
            }
        }

        /// <summary>
        /// Execute this code before a task item is inserted
        /// Convert dates to us dates (otherwise it does not work due to a ms bug)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvTask_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (e.Values["DueDate"] != null)
            {
                e.Values["DueDate"] = GUIHelper.GetUSDate(e.Values["DueDate"].ToString());
            }

            //TODO: shouldn't this be added in BL?
            e.Values["CreatedOn"] = GUIHelper.GetUSDate(DateTime.Now.ToString());
            
            //TODO: shouldn't this be added in BL?
            e.Values["CreatedBy"] = Membership.GetUser().UserName;

        }

        /// <summary>
        /// Execute this code before updating a task item.
        /// Again mostly date conversions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvTask_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (e.NewValues["DueDate"] != null)
                e.NewValues["DueDate"] = GUIHelper.GetUSDate(e.NewValues["DueDate"].ToString());
            if (e.NewValues["CreatedOn"] != null)
            {
                e.NewValues["CreatedOn"] = GUIHelper.GetUSDate(e.NewValues["CreatedOn"].ToString());
            }

            //TODO: move to bl?
            e.NewValues["ChangedOn"] = GUIHelper.GetUSDate(DateTime.Now.ToString());
            e.NewValues["ChangedBy"] = Membership.GetUser().UserName;
        }

        /// <summary>
        /// Execute this code before a task item is retrieved from db.
        /// Update the input parameters for the method which gets the item by the id from the query string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsTasks_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                e.InputParameters["Id"] = Request.QueryString["Id"];
            }
        }

        /// <summary>
        /// Execute this code when an item is inserted
        /// redirects to the tasklist page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvTask_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            Response.Redirect("~/GUI/TaskManager/TaskList.aspx");
        }

        /// <summary>
        /// Execute this code when an item is updated
        /// redirects to the tasklist page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvTask_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            Response.Redirect("~/GUI/TaskManager/TaskList.aspx");
        }

        /// <summary>
        /// Redirect to tasklist page when cancelling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dtvTask_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit)
            {
                e.Cancel = true;
                Response.Redirect("/GUI/TaskManager/TaskList.aspx");
            }
        }
    }
}