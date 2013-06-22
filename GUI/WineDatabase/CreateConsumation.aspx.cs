using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace EbalitWebForms.GUI.WineDatabase
{
    public partial class CreateConsumation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Request.QueryString["Id"] != null)
                {
                    //edit mode
                    this.dvwConsumation.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                    //insert mode
                    this.dvwConsumation.ChangeMode(DetailsViewMode.Insert);
                }
               
                ////Make sure drop down list wine shows multiple columns
                //DropDownList ddlWine = (DropDownList)GUIHelper.RecursiveFindControl(dvwConsumation, "ddlWine");
                //if (ddlWine != null)
                //{
                //    var items = odsWines.Select();
                //    ListItem[] DisplayText = items.Cast<DataLayer.Wine>().Select(cc => 
                //        new ListItem( cc.Label + ", " + cc.Year + ", " + cc.Grape + ", " + cc.Origin, cc.Id.ToString())).ToArray();
                //    ddlWine.Items.AddRange(DisplayText);
                //}
                

                //prefil Taster with logged in user
                Label lblTaster = (Label)GUIHelper.RecursiveFindControl(dvwConsumation, "lblTaster");
                if (lblTaster != null)
                {
                    lblTaster.Text = Membership.GetUser().UserName;
                    
                }

            }
        }

        protected void odsConsumation_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                //Set the input parameter Id of the GetItemById method to the Id in the query string
                e.InputParameters["Id"] = Request.QueryString["Id"];
            }

           
        }

        protected void dvwConsumation_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            Response.Redirect("/GUI/WineDatabase/ManageConsumations.aspx");
        }

        protected void dvwConsumation_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            Response.Redirect("/GUI/WineDatabase/ManageConsumations.aspx");
        }

        protected void dvwConsumation_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            //if bought on is not empty, make sure it is converted to US date (workaround for a bug in control)
            if (e.NewValues["TastedOn"] != null)
            {
                e.NewValues["TastedOn"] = GUIHelper.GetUSDate(e.NewValues["TastedOn"].ToString());
            }
            //Set changed on and changed by
            e.NewValues["ChangedOn"] = DateTime.Now;
            e.NewValues["ChangedBy"] = Membership.GetUser().UserName;

            //for some reason I don't know now, I need to convert the existing createdOn date to a date again
            //otherwise a convertion error is thrown
            e.NewValues["CreatedOn"] = Convert.ToDateTime(e.OldValues["CreatedOn"]);
        }

        protected void dvwConsumation_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //if bought on is not empty, make sure it is converted to US date (workaround for a bug in control)
            if (e.Values["TastedOn"] != null)
            {
                e.Values["TastedOn"] = GUIHelper.GetUSDate(e.Values["TastedOn"].ToString());
            }
            //set created on and created by
            e.Values["CreatedOn"] = DateTime.Now;
            e.Values["CreatedBy"] = Membership.GetUser().UserName;
        }

        protected void odsConsumation_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }

        protected void dvwConsumation_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit)
            {
                e.Cancel = true;
                Response.Redirect("/GUI/WineDatabase/ManageConsumations.aspx");
            }

        }
    }
}