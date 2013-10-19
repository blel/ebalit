using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.Controlling
{
    public partial class WorkBreakdownStructure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.trvProjectView.DataSource = new BusinessLogicLayer.Controlling.ProjectDataSource();
            this.trvProjectView.DataBind();
        }

        protected void trvProjectView_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
    }
}