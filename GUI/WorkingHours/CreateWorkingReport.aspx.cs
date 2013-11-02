using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WorkingHours
{
    public partial class CreateWorkingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //make sure details is in either edit mode (id available) or insert mode
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    //edit mode
                    this.dtvWorkingReport.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                    //insert mode
                    this.dtvWorkingReport.ChangeMode(DetailsViewMode.Insert);
                }
            }
        }
    }
}