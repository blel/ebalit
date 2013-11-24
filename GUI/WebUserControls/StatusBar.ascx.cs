using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WebUserControls
{
    public partial class StatusBar : System.Web.UI.UserControl
    {
        public bool ClearOnPostback{ get; set; }

        public string StatusText
        {
            get { return lblStatus.Text; }
            set { lblStatus.Text = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ClearOnPostback)
                {
                    StatusText = string.Empty;
                }
            }
        }
    }
}