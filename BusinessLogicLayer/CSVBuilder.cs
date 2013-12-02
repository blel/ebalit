using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Text;
using System.IO;

namespace EbalitWebForms.BusinessLogicLayer
{
    /// <summary>
    /// Code found here: http://stackoverflow.com/questions/1179816/best-practices-for-serializing-objects-to-a-custom-string-format-for-use-in-an-o
    /// Converts a list of types to a csv file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSVBuilder
    {
        /// <summary>
        /// Converts the objectlist to csv
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="separator"></param>
        /// <param name="objectlist"></param>
        /// <returns></returns>
        public static string ToCsv<T>(string separator, IEnumerable<T> objectlist)
        {
            var t = typeof(T);
            var fields = t.GetProperties();
            var header = String.Join(separator, fields.Select(f => f.Name).ToArray());

            var csvdata = new StringBuilder();
            csvdata.AppendLine(header);

            foreach (var item in objectlist)
                csvdata.AppendLine(ToCsvFields(separator, fields, item));

            return csvdata.ToString();
        }

        /// <summary>
        /// adds a new line with the values of each item 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="separator"></param>
        /// <param name="properties"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static string ToCsvFields<T>(string separator, PropertyInfo[] properties, T item)
        {
            var csvLine = new StringBuilder();

            foreach (var property in properties)
            {
                if (csvLine.Length > 0)
                    csvLine.Append(separator);

                var propertyValue = property.GetValue(item, null);

                if (propertyValue != null)
                    csvLine.Append("\"" + propertyValue + "\"");
            }

            return csvLine.ToString();
        }



    }

}