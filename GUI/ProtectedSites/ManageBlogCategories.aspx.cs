using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.ProtectedSites
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            odsBlogCategories.Deleted += new ObjectDataSourceStatusEventHandler( odsBlogCategories_Deleted);

        }

        void odsBlogCategories_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (!(bool)e.ReturnValue)
            {

                StatusLine.InnerHtml = "<p>Some error occurred: The delete operation failed. Check if there are blog entries for that category. </p>";
                
            }

            
        }
    }
}