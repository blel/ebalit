using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI 
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkTaskCategories_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("~/GUI/TaskCategories.aspx");
        }

        protected void lnkCreateTask_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("~/GUI/TaskManager/TaskDetail.aspx");
        }

        protected void lnkTaskList_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("~/GUI/TaskManager/TaskList.aspx");
        }
    }
}