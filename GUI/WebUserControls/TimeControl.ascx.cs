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

        public event EventHandler TimeChanged;

        public DateTime DisplayTime { 
            get
            {
                _displayTime = new DateTime(_displayTime.Year, _displayTime.Month, _displayTime.Day,
                    Convert.ToInt32(this.ddlHour.SelectedValue), Convert.ToInt32(this.ddlMinute.SelectedValue), 0);
                return _displayTime;
            }
            set {
                _displayTime = value;
                ddlHour.SelectedValue = _displayTime.Hour.ToString().PadLeft(2, '0');
                ddlMinute.SelectedValue = _displayTime.Minute.ToString().PadLeft(2, '0');
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Raises the time changed event when ddl is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlHour_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //raise the time changed event
            TimeChanged(sender, e);
        }

        /// <summary>
        /// Raises the time changed event when ddl is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMinute_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //raise the time changed event
            TimeChanged(sender, e);
        }
    }
}