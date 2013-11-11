using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WebUserControls
{
    public partial class TimeControl : System.Web.UI.UserControl
    {
        private DateTime _displayTime;
        
        public DateTime DisplayTime { 
            get
            {
                _displayTime = new DateTime(_displayTime.Year, _displayTime.Month, _displayTime.Day,
                    Convert.ToInt32(this.ddlHour.SelectedValue), Convert.ToInt32(this.ddlMinute.SelectedValue), 0);
                return _displayTime;
            }
            set {
                _displayTime = value;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }



        
        
    }
}