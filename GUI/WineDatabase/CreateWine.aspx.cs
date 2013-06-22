using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace EbalitWebForms.GUI.WineDatabase
{
    /// <summary>
    /// Codebehind of CreateWine asp view
    /// </summary>
    public partial class CreateWine : System.Web.UI.Page
    {
        #region Protected Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            //if site is really loaded and no postback
            if (!IsPostBack)
            {
                //if there is a "Id" parameter in the url change to edit mode
                if (Request.QueryString["Id"] != null)
                {
                    this.dvwWine.ChangeMode(DetailsViewMode.Edit);
                }
                //otherwise go to insert mode
                else
                {
                    this.dvwWine.ChangeMode(DetailsViewMode.Insert);
                }
            }
        }

        /// <summary>
        /// Event fired when a new record is inserted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvwWine_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //if bought on is not empty, make sure it is converted to US date (workaround for a bug in control)
            if (e.Values["BoughtOn"] != null)
            {
                e.Values["BoughtOn"] = GUIHelper.GetUSDate(e.Values["BoughtOn"].ToString());
            }
            //set created on and created by
            e.Values["CreatedOn"] = DateTime.Now;
            e.Values["CreatedBy"] = Membership.GetUser().UserName;
        }

        /// <summary>
        /// if there is a parameter Id in the url, make sure the record with that id is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsWines_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                //the input parameters refers to the GetItemById(int id) method in the repository
                e.InputParameters["Id"] = Request.QueryString["Id"];
            }
        }

        /// <summary>
        /// Event thrown when the mode of the details view changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvwWine_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            //if edit is canceled, route back to the list screen
            if (e.CancelingEdit)
            {
                e.Cancel = true;
                Response.Redirect("/GUI/WineDatabase/ManageWines.aspx");
            }
        }

        /// <summary>
        /// Event fired before updating a wine detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvwWine_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            //if bought on is not empty, make sure it is converted to US date (workaround for a bug in control)
            if (e.NewValues["BoughtOn"] != null)
            {
                e.NewValues["BoughtOn"] = GUIHelper.GetUSDate(e.NewValues["BoughtOn"].ToString());
            }
            //Set changed on and changed by
            e.NewValues["ChangedOn"] = DateTime.Now;
            e.NewValues["ChangedBy"] = Membership.GetUser().UserName;

            //for some reason I don't know now, I need to convert the existing createdOn date to a date again
            //otherwise a convertion error is thrown
            e.NewValues["CreatedOn"] = Convert.ToDateTime(e.OldValues["CreatedOn"]);
        }

        /// <summary>
        /// Event fired after insert of Wine Details View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvwWine_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {

            //redirect to the manage wines screen
            Response.Redirect("/GUI/WineDatabase/ManageWines.aspx");
        }

        /// <summary>
        /// Event fired after update of Wine Details View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvwWine_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            CheckBoxList cblAttributes = (CheckBoxList)dvwWine.FindControl("cblAttributes");
            if (cblAttributes != null)
            {
                ClearAttributeAssignments(Convert.ToInt32(e.Keys[0]));
                SetAttributeAssignments(cblAttributes, Convert.ToInt32(e.Keys[0]));
            }

            //redirect to the manage wines screen
            Response.Redirect("/GUI/WineDatabase/ManageWines.aspx");
        }

        /// <summary>
        /// Object Data Source event fired when a record is inserted
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsWines_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            CheckBoxList cblAttributes = (CheckBoxList)dvwWine.FindControl("cblAttributes");
            if (cblAttributes != null)
            {
                ClearAttributeAssignments(Convert.ToInt32(e.ReturnValue));
                SetAttributeAssignments(cblAttributes, Convert.ToInt32(e.ReturnValue));
            }
        }

        /// <summary>
        /// Event fired when data is bound to the details view
        /// In this event, the checkbox values are loaded from db and updated in the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvwWine_DataBound(object sender, EventArgs e)
        {
            CheckBoxList cblAttributes = (CheckBoxList)dvwWine.FindControl("cblAttributes");
            if (cblAttributes != null && dvwWine.DataItem != null)
            {
                //get current Id
                int currentId = ((DataLayer.Wine)dvwWine.DataItem).Id;
                //Create the WineToWineAttribute repository
                BusinessLogicLayer.Repository<DataLayer.WineToWineAttribute> attributeAssignmentRepository =
                    new BusinessLogicLayer.Repository<DataLayer.WineToWineAttribute>();
                //retrieve all attribute assignments
                IEnumerable<DataLayer.WineToWineAttribute> attributeAssignments =
                    attributeAssignmentRepository.GetItems().Where(cc => cc.FK_Wine == currentId);
                //set the checkboxes
                cblAttributes.Items.SetSelectedItems(attributeAssignments.Select(cc => cc.FK_WineAttribute).ToList());

            }
        }

        #endregion

        #region private methods
        /// <summary>
        /// Deletes any attributes assignments of the wine with id = wineID
        /// </summary>
        /// <param name="wineId"></param>
        private void ClearAttributeAssignments(int wineId)
        {
            BusinessLogicLayer.Repository<DataLayer.WineToWineAttribute> attributeAssignmentRepository = new BusinessLogicLayer.Repository<DataLayer.WineToWineAttribute>();

            IEnumerable<DataLayer.WineToWineAttribute> currentAttributes =
                new BusinessLogicLayer.Repository<DataLayer.WineToWineAttribute>().GetItems().Where(cc => cc.FK_Wine == wineId);

            foreach (var currentAttribute in currentAttributes)
            {
                attributeAssignmentRepository.DeleteItem(currentAttribute);
            }
        }

        /// <summary>
        /// Creates all attribute assigments found in the given checkboxlist for wine with id = wineID
        /// </summary>
        /// <param name="cblAttributes"></param>
        /// <param name="wineId"></param>
        private void SetAttributeAssignments(CheckBoxList cblAttributes, int wineId)
        {
            BusinessLogicLayer.Repository<DataLayer.WineToWineAttribute> attributeAssignmentRepository = new BusinessLogicLayer.Repository<DataLayer.WineToWineAttribute>();

            foreach (var selectedAttribute in cblAttributes.Items.GetSelectedItems())
            {
                DataLayer.WineToWineAttribute newAttribute = new DataLayer.WineToWineAttribute();
                newAttribute.FK_Wine = wineId;// Convert.ToInt32(e.Keys[0]);
                newAttribute.FK_WineAttribute = Convert.ToInt32(selectedAttribute.Value);
                attributeAssignmentRepository.CreateItem(newAttribute);
            }
        }
        #endregion

    }
}