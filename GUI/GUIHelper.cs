using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EbalitWebForms.GUI
{
    public static class GUIHelper
    {
        public static string GetUSDate(string anyDate)
        {
            DateTime currentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(anyDate) && DateTime.TryParse(anyDate, out currentDate))
            {
                return currentDate.ToString(new CultureInfo("en-US"));
            }
            return string.Empty;
        }

        /// <summary>
        /// Searches for a control recursively
        /// </summary>
        /// <param name="control"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static Control RecursiveFindControl(Control control, string controlName)
        {
            if (control.ID == controlName)
                return control;

            foreach (Control childControl in control.Controls)
            {
                Control t = RecursiveFindControl(childControl, controlName);
                if (t != null) return t;
            }
            return null;
        }
    }
}