using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

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
    }
}